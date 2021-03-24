using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuleServer.Data;
using RuleServer.Helpers.ExpressionParser;
using RuleServer.Models.Expression;
using RuleServer.Models.RuleService;

namespace RuleServer.Services
{
    // singleton
    public class RuleService<TSensorId> : IHostedService
    {
        private List<RuleSettingsCompiled> _ruleSet = new();
        private Dictionary<TSensorId, List<RuleSettingsCompiled>> _sensorId_ruleSet = new();
        private readonly IOptionsMonitor<RuleServiceSettings> _settingsMonitor;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<RuleService<TSensorId>> _logger;
        private RuleServiceSettings _settings;

        public RuleService(
            IOptionsMonitor<RuleServiceSettings> settingsMonitor,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RuleService<TSensorId>> logger)
        {
            _settingsMonitor = settingsMonitor;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _settingsMonitor.OnChange(UpdateSettings);
            UpdateSettings(_settingsMonitor.CurrentValue);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task AlertAsync(IDictionary<string, object> symbolTable,
            Action<RuleSettingsCompiled, IDictionary<string, object>> action)
        {
            HashSet<RuleSettingsCompiled> visited = new();
            if (symbolTable.ContainsKey("sensorId"))
            {
                if (!_sensorId_ruleSet.ContainsKey((TSensorId)symbolTable["sensorId"]))
                {
                    return;
                }
                var relativeRuleSet = _sensorId_ruleSet[(TSensorId)symbolTable["sensorId"]];
                await AlertAsync(relativeRuleSet, symbolTable, visited, action).ConfigureAwait(false);
            }
            else
            {
                await AlertAsync(_ruleSet, symbolTable, visited, action).ConfigureAwait(false);
            }
        }

        private static async Task AlertAsync(
            List<RuleSettingsCompiled> ruleSet,
            IDictionary<string, object> symbolTable,
            HashSet<RuleSettingsCompiled> visited,
            Action<RuleSettingsCompiled, IDictionary<string, object>> action)
        {
            foreach (var rule in ruleSet)
            {
                // prevent multiple visits
                if (visited.Contains(rule))
                {
                    continue;
                }
                visited.Add(rule);
                // check if condition for alert hits
                var booleanValue = (bool)rule.ExpressionTree.GetValue(symbolTable);
                if (booleanValue)
                {
                    Task actionTask = null;
                    lock (rule)
                    {
                        rule.IncrementHitCount();
                        if (rule.HitCount == 0)
                        {
                            actionTask = Task.Run(() => action(rule, symbolTable));
                        }
                    }
                    if (actionTask != null)
                    {
                        await actionTask.ConfigureAwait(false);
                    }
                    return;
                }
            }
        }

        public async void LogAlert(RuleSettingsCompiled rule, IDictionary<string, object> request)
        {
            _logger.LogDebug("Start writing database.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using var scope = _serviceScopeFactory.CreateScope();
            var logDatabase = scope.ServiceProvider.GetService<RuleAlertContext>();
            await logDatabase.RuleAlerts.AddAsync(new()
            {
                IsActive = true,
                RuleDetail = rule.ConditionExpression,
                RuleName = rule.RuleName,
                Timestamp = (string)request["timestamp"],
                SensorId = (string)request["sensorId"],
            }).ConfigureAwait(false);
            await logDatabase.SaveChangesAsync().ConfigureAwait(false);
            stopwatch.Stop();
            _logger.LogDebug($"Done writing database. Time span: {stopwatch.Elapsed.TotalSeconds:0.####}s");
        }

        private void UpdateSettings(RuleServiceSettings settings)
        {
            _settings = settings;
            _logger.LogInformation("Updating settings...");
            List<RuleSettingsCompiled> newRuleSet = new();
            foreach (var rule in settings.RuleSet)
            {
                var ruleCompiled = ParseRule(rule);
                newRuleSet.Add(ruleCompiled);
            }
            lock (this)
            {
                _ruleSet = newRuleSet;
            }
            UpdateIndex();
            _logger.LogInformation("Done updating settings.");
        }

        private static RuleSettingsCompiled ParseRule(RuleSettings rule)
        {
            ICharStream stream = CharStreams.fromString(rule.ConditionExpression);
            ITokenSource lexer = new RuleConditionLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            RuleConditionParser parser = new(tokens)
            {
                BuildParseTree = true
            };
            IParseTree tree = parser.start();

            RuleConditionVisitor visitor = new();
            ISimpleExpression query = (ISimpleExpression)visitor.Visit(tree);

            return new RuleSettingsCompiled(rule)
            {
                ExpressionTree = query
            };
        }

        // update _sensorId_ruleSet
        private void UpdateIndex()
        {
            Dictionary<TSensorId, List<RuleSettingsCompiled>> sensorId_ruleSet = new();

            foreach (var rule in _ruleSet)
            {
                var sensorIds = GetSensorIds(rule.ExpressionTree);
                foreach (var sensorId in sensorIds)
                {
                    if (sensorId_ruleSet.ContainsKey(sensorId))
                    {
                        sensorId_ruleSet[sensorId].Add(rule);
                    }
                    else
                    {
                        sensorId_ruleSet[sensorId] = new()
                        {
                            rule
                        };
                    }
                }
            }

            lock (this)
            {
                _sensorId_ruleSet = sensorId_ruleSet;
            }
        }

        private HashSet<TSensorId> GetSensorIds(ISimpleExpression expression)
        {
            HashSet<TSensorId> results = new();
            if (expression is SimpleExpressionConstant ||
                expression is SimpleExpressionParameter)
            {
                return new();
            }
            else if (expression is SimpleExpressionUnary)
            {
                return GetSensorIds(expression);
            }
            else if (expression is SimpleExpressionBinary expressionBinary)
            {
                // terminal condition
                if (expressionBinary.Operator == SimpleExpressionBinaryOperator.Equal &&
                    expressionBinary.LeftOperand is SimpleExpressionParameter leftExpressionParameter &&
                    leftExpressionParameter.ParameterName == "sensorId" &&
                    expressionBinary.RightOperand is SimpleExpressionConstant rightExpressionConstant)
                {
                    results.Add((TSensorId)rightExpressionConstant.Value);
                    return results;
                }
                else if (
                    expressionBinary.Operator == SimpleExpressionBinaryOperator.Equal &&
                    expressionBinary.RightOperand is SimpleExpressionParameter rightExpressionParameter &&
                    rightExpressionParameter.ParameterName == "sensorId" &&
                    expressionBinary.LeftOperand is SimpleExpressionConstant leftExpressionConstant)
                {
                    results.Add((TSensorId)leftExpressionConstant.Value);
                    return results;
                }

                // regression
                var leftResult = GetSensorIds(expressionBinary.LeftOperand);
                var rightResult = GetSensorIds(expressionBinary.RightOperand);

                results.UnionWith(leftResult);
                results.UnionWith(rightResult);
            }
            return results;
        }
    }
}

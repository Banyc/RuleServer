using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuleServer.Data;
using RuleServer.Models.Expression;
using RuleServer.Models.RuleService;

namespace RuleServer.Services
{
    // singleton
    public partial class RuleService : IHostedService
    {
        public string ServerName { get => _settings.ServerName; }
        private List<RuleSettingsCompiled> _ruleSet = new();
        private HashSet<ISimpleExpression> _duplicatedSubtress;
        private Dictionary<object, List<RuleSettingsCompiled>> _sensorId_ruleSet = new();
        private readonly IOptionsMonitor<RuleServiceSettings> _settingsMonitor;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<RuleService> _logger;
        private RuleServiceSettings _settings;

        public RuleService(
            IOptionsMonitor<RuleServiceSettings> settingsMonitor,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RuleService> logger)
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

        public void Match(IDictionary<string, object> symbolTable,
            Action<RuleService, MatchedActionArgs> action)
        {
            if (symbolTable.ContainsKey("sensorId"))
            {
                if (!_sensorId_ruleSet.ContainsKey(symbolTable["sensorId"]))
                {
                    return;
                }
                var relativeRuleSet = _sensorId_ruleSet[symbolTable["sensorId"]];
                Match(relativeRuleSet, symbolTable, action);
            }
            else
            {
                Match(_ruleSet, symbolTable, action);
            }
        }

        private void Match(
            List<RuleSettingsCompiled> ruleSet,
            IDictionary<string, object> symbolTable,
            Action<RuleService, MatchedActionArgs> action)
        {
            HashSet<RuleSettingsCompiled> visited = new();
            Dictionary<ISimpleExpression, object> computedValues = new();
            foreach (var rule in ruleSet)
            {
                // prevent multiple visits
                if (visited.Contains(rule))
                {
                    continue;
                }
                visited.Add(rule);
                // check if condition for matching

                bool booleanValue;
                if (_duplicatedSubtress == null)
                {
                    // expression not optimized
                    booleanValue = (bool)rule.ExpressionTree.GetValue(symbolTable);
                }
                else
                {
                    // expression optimized
                    booleanValue = (bool)GetExpressionValue(rule.ExpressionTree,
                                                            symbolTable,
                                                            computedValues,
                                                            _duplicatedSubtress);
                }
                if (booleanValue)
                {
                    bool isReact = false;
                    lock (rule)
                    {
                        rule.IncrementHitCount();
                        if (rule.HitCount == 0)
                        {
                            isReact = true;
                        }
                    }
                    if (isReact)
                    {
                        MatchedActionArgs args = new()
                        {
                            Rule = rule,
                            Arguments = symbolTable,
                        };
                        action(this, args);
                    }
                    return;
                }
            }
        }

        private static object GetExpressionValue(
            ISimpleExpression expressionTree,
            IDictionary<string, object> symbolTable,
            IDictionary<ISimpleExpression, object> computedValues,
            HashSet<ISimpleExpression> duplicatedSubtrees)
        {
            if (computedValues.ContainsKey(expressionTree))
            {
                return computedValues[expressionTree];
            }
            if (expressionTree is SimpleExpressionBinary ||
                expressionTree is SimpleExpressionUnary)
            {
                object value = null;
                if (expressionTree is SimpleExpressionBinary binary)
                {
                    object leftValue = GetExpressionValue(binary.LeftOperand,
                                                          symbolTable,
                                                          computedValues,
                                                          duplicatedSubtrees);
                    object rightValue = GetExpressionValue(binary.RightOperand,
                                                           symbolTable,
                                                           computedValues,
                                                           duplicatedSubtrees);
                    value = binary.GetValue(symbolTable, leftValue, rightValue);
                }
                else if (expressionTree is SimpleExpressionUnary unary)
                {
                    object operandValue = GetExpressionValue(unary.Operand,
                                                             symbolTable,
                                                             computedValues,
                                                             duplicatedSubtrees);
                    value = unary.GetValue(symbolTable, operandValue);
                }
                if (duplicatedSubtrees.Contains(expressionTree))
                {
                    computedValues[expressionTree] = value;
                }
                return value;
            }
            else
            {
                return expressionTree.GetValue(symbolTable);
            }
        }
    }
}

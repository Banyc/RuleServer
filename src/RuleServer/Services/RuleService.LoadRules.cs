using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.Logging;
using RuleServer.Helpers.ExpressionParser;
using RuleServer.Models.Expression;
using RuleServer.Models.RuleService;

namespace RuleServer.Services
{
    public partial class RuleService<TSensorId>
    {
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

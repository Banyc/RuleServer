using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.Logging;
using RuleServer.Helpers;
using RuleServer.Helpers.ExpressionParser;
using RuleServer.Models.Expression;
using RuleServer.Models.RuleService;

namespace RuleServer.Services
{
    public partial class RuleService
    {
        private void UpdateSettings(RuleServiceSettings settings)
        {
            _settings = settings;
            _logger.LogInformation("Updating settings...");
            Dictionary<string, RuleGroupCompiled> newRuleGroups = new();
            foreach (var group in settings.RuleGroups)
            {
                List<RuleSettingsCompiled> newRuleSet = new();
                foreach (var rule in group.RuleSet)
                {
                    var ruleCompiled = ParseRule(rule);
                    newRuleSet.Add(ruleCompiled);
                }
                // optimize
                var duplicatedSubtree = Optimize(newRuleSet);
                // make new group
                RuleGroupCompiled newGroup = new()
                {
                    GroupName = group.GroupName,
                    RuleSet = newRuleSet,
                    DuplicatedSubtrees = duplicatedSubtree,
                    IndexedParameters = group.IndexedParameters,
                };
                // update index
                UpdateIndex(newGroup);
                // add new rules to rule groups
                newRuleGroups[group.GroupName] = newGroup;
            }
            // switch
            lock (this)
            {
                _ruleGroups = newRuleGroups;
            }

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

        // TODO
        private void UpdateIndex(RuleGroupCompiled ruleGroup)
        {
            // build min-terms
            foreach (var rule in ruleGroup.RuleSet)
            {
                if (rule.ExpressionTree is SimpleExpressionBinary binary)
                {
                    SimpleExpressionParataxis root = SimpleExpressionParataxis.Flatten(binary);
                    rule.MinTerms = root;
                }
                else
                {
                    rule.MinTerms = rule.ExpressionTree;
                }
            }

            // build index in rule group
            // TODO
        }

        private HashSet<ISimpleExpression> Optimize(List<RuleSettingsCompiled> ruleSettingsList)
        {
            List<ISimpleExpression> expressions =
                ruleSettingsList.ConvertAll(xxxx => xxxx.ExpressionTree);
            HashSet<ISimpleExpression> duplicatedSubtress =
                ExpressionTreeOptimizer.RemoveDuplicatedSubtrees(expressions);
            int i;
            for (i = 0; i < ruleSettingsList.Count; i++)
            {
                RuleSettingsCompiled ruleSettings = ruleSettingsList[i];
                ruleSettings.ExpressionTree = expressions[i];
            }
            return duplicatedSubtress;
        }

        // find any `sensorId == xxx` in expression
        private HashSet<object> GetSensorIds(ISimpleExpression expression)
        {
            HashSet<object> results = new();
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
                    results.Add(rightExpressionConstant.Value);
                    return results;
                }
                else if (
                    expressionBinary.Operator == SimpleExpressionBinaryOperator.Equal &&
                    expressionBinary.RightOperand is SimpleExpressionParameter rightExpressionParameter &&
                    rightExpressionParameter.ParameterName == "sensorId" &&
                    expressionBinary.LeftOperand is SimpleExpressionConstant leftExpressionConstant)
                {
                    results.Add(leftExpressionConstant.Value);
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

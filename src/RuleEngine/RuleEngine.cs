using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RuleEngine.Models.Expression;
using RuleEngine.Models.RuleEngine;

namespace RuleEngine
{
    // singleton
    public partial class RuleEngine : IDisposable
    {
        public string ServerName { get => _settings.ServerName; }
        private Dictionary<string, RuleGroupCompiled> _ruleGroups;
        private readonly ILogger<RuleEngine> _logger;
        private RuleEngineSettings _settings;

        public RuleEngine(
            RuleEngineSettings settings,
            ILogger<RuleEngine> logger)
        {
            _settings = settings;
            _logger = logger;

            // compile the rules in the settings
            UpdateSettings(settings);
        }

        public RuleEngine(
            RuleEngineSettings settings)
        {
            _settings = settings;

            // compile the rules in the settings
            UpdateSettings(settings);
        }

        public void Match(string groupName, IDictionary<string, object> symbolTable,
            Action<RuleEngine, MatchedActionArgs> matchingAction,
            Action<RuleEngine, ExceptionActionArgs> exceptionAction)
        {
            Match(_ruleGroups[groupName], symbolTable, matchingAction, exceptionAction);
        }

        public void Dispose()
        {
            foreach (var group in _ruleGroups.Values)
            {
                group.CachedIndex.Dispose();
            }
        }

        #region private methods
        private void Match(
            RuleGroupCompiled ruleGroup,
            IDictionary<string, object> symbolTable,
            Action<RuleEngine, MatchedActionArgs> matchingAction,
            Action<RuleEngine, ExceptionActionArgs> exceptionAction)
        {
#if DEBUG
            HashSet<RuleSettingsCompiled> visited = new();
#endif
            Dictionary<ISimpleExpression, object> computedValues = new();

            // look up index + get rules
            IEnumerable<RuleSettingsCompiled> rulesToVisit =
                ruleGroup.GetMatchedRules(symbolTable);

            // visit rules
            foreach (var rule in rulesToVisit)
            {
#if DEBUG
                // DEBUG ONLY: prevent multiple visits
                if (visited.Contains(rule))
                {
                    throw new Exception("The rules should not be duplicated");
                    continue;
                }
                visited.Add(rule);
#endif

                // check if condition for matching {
                bool booleanValue = false;
                try
                {
                    if (ruleGroup.DuplicatedSubtrees == null)
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
                                                                ruleGroup.DuplicatedSubtrees);
                    }
                }
                catch (Exception ex)
                {
                    // ISSUE: huge time consuming
                    // symbol table does not contain enough arguments for the expression of this rule.
                    _logger?.LogDebug(ex.Message);
                    ExceptionActionArgs args = new()
                    {
                        Group = ruleGroup,
                        Arguments = symbolTable,
                        Exception = ex,
                        Rule = rule,
                    };
                    if (exceptionAction != null)
                    {
                        exceptionAction(this, args);
                    }
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
                            Group = ruleGroup,
                            Rule = rule,
                            Arguments = symbolTable,
                        };
                        if (matchingAction != null)
                        {
                            matchingAction(this, args);
                        }
                    }
                }
                // }
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
        #endregion
    }
}

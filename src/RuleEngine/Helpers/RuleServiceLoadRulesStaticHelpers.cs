using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using RuleEngine.Helpers.ExpressionParser;
using RuleEngine.Models.Expression;
using RuleEngine.Models.RuleService;

namespace RuleEngine.Helpers
{
    public static class RuleServiceLoadRulesStaticHelpers
    {
        public static RuleSettingsCompiled ParseRule(RuleSettings rule)
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

        public static void UpdateIndexByMinTerm(
            ISimpleExpression minTerm,
            RuleGroupCompiled ruleGroup,
            RuleSettingsCompiled rule)
        {
            if (minTerm is SimpleExpressionParataxis childUnderOrParataxis &&
                childUnderOrParataxis.Operator == SimpleExpressionBinaryOperator.And)
            {
                // children of `minTerm` are under `and` operator
                HashSet<string> visitedIndexedParameters = new();
                foreach (var childUnderAnd in childUnderOrParataxis.Operands)
                {
                    string visitedIndexedParameter =
                        UpdateIndexUnderAnd(childUnderAnd, ruleGroup, rule);

                    if (visitedIndexedParameter != null)
                    {
                        visitedIndexedParameters.Add(visitedIndexedParameter);
                    }
                }
                // index this rule if whether to index by a parameter is uncertain
                UpdateUncertainIndex(visitedIndexedParameters, ruleGroup, rule);
            }
            else
            {
                // `minTerm` is not `and` node
                HashSet<string> visitedIndexedParameters = new();

                string visitedIndexedParameter =
                    UpdateIndexUnderAnd(minTerm, ruleGroup, rule);

                if (visitedIndexedParameter != null)
                {
                    visitedIndexedParameters.Add(visitedIndexedParameter);
                }

                UpdateUncertainIndex(visitedIndexedParameters, ruleGroup, rule);
            }
        }

        // return visitedIndexedParameter
        public static string UpdateIndexUnderAnd(
            ISimpleExpression childUnderAnd,
            RuleGroupCompiled ruleGroup,
            RuleSettingsCompiled rule)
        {
            string visitedIndexedParameter = null;
            if (childUnderAnd is SimpleExpressionBinary thirdLayerBinary &&
                thirdLayerBinary.Operator == SimpleExpressionBinaryOperator.Equal)
            {
                // children are under `==` operator
                string parameterName = null;
                object targetValue = null;
                void SetInfo(ISimpleExpression child)
                {
                    if (child is SimpleExpressionParameter parameterNode)
                    {
                        parameterName = parameterNode.ParameterName;
                    }
                    if (child is SimpleExpressionConstant constantNode)
                    {
                        targetValue = constantNode.Value;
                    }
                }
                SetInfo(thirdLayerBinary.LeftOperand);
                SetInfo(thirdLayerBinary.RightOperand);
                if (parameterName == null || targetValue == null)
                {
                    // `thirdLayerBinary` is not a valid node
                }
                else
                {
                    // index this rule
                    visitedIndexedParameter = parameterName;
                    ruleGroup.IndexByParameterName[parameterName].AddToIndexByValue(targetValue, rule);
                }
            }
            return visitedIndexedParameter;
        }

        public static void UpdateUncertainIndex(
            HashSet<string> visitedIndexedParameters,
            RuleGroupCompiled ruleGroup,
            RuleSettingsCompiled rule)
        {
            var notVisitedParameters = ruleGroup.IndexedParameters.Except(visitedIndexedParameters);
            foreach (var notVisitedParameter in notVisitedParameters)
            {
                ruleGroup.IndexByParameterName[notVisitedParameter].AddToUncertainRules(rule);
            }
        }

        // TODO
        public static void UpdateIndex(RuleGroupCompiled ruleGroup)
        {
            if (ruleGroup.IndexedParameters == null)
            {
                // user does not define which parameter to index.
                return;
            }

            // build min-terms
            foreach (var rule in ruleGroup.RuleSet)
            {
                if (rule.ExpressionTree is SimpleExpressionBinary binaryNode)
                {
                    SimpleExpressionParataxis root = SimpleExpressionParataxis.GetMinTerms(binaryNode);
                    rule.MinTerms = root ?? rule.ExpressionTree;
                }
                else
                {
                    rule.MinTerms = rule.ExpressionTree;
                }
            }

            // build index in rule group
            // TODO: review
            foreach (string indexedParameter in ruleGroup.IndexedParameters)
            {
                ruleGroup.IndexByParameterName[indexedParameter] = new()
                {
                    ParameterName = indexedParameter,
                };
            }
            foreach (var rule in ruleGroup.RuleSet)
            {
                if (rule.MinTerms is SimpleExpressionParataxis rootParataxis)
                {
                    if (rootParataxis.Operator == SimpleExpressionBinaryOperator.Or)
                    {
                        // children are under `or` operator
                        foreach (var childUnderOr in rootParataxis.Operands)
                        {
                            UpdateIndexByMinTerm(childUnderOr, ruleGroup, rule);
                        }
                    }
                    else if (rootParataxis.Operator == SimpleExpressionBinaryOperator.And)
                    {
                        UpdateIndexByMinTerm(rootParataxis, ruleGroup, rule);
                    }
                    else
                    {
                        throw new System.Exception("Not possible to be here");
                    }
                }
                else
                {
                    UpdateIndexByMinTerm(rule.ExpressionTree, ruleGroup, rule);
                }
            }

            // merge uncertain rules to index
            foreach (var kv in ruleGroup.IndexByParameterName)
            {
                kv.Value.MergeUncertainRulesToIndexByValue();
            }
        }

        public static HashSet<ISimpleExpression> Optimize(List<RuleSettingsCompiled> ruleSettingsList)
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

    }
}
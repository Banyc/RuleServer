using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Models.Expression;
using RuleEngine.Models.RuleEngine;
using RuleEngine.Tests.Extensions;
using Xunit;
using System.Linq;

namespace RuleEngine.Tests
{
    public class MinTermsTests
    {
        [Fact]
        private void TestMinTerms()
        {
            RuleSettings rule = new()
            {
                ConditionExpression = "(a || b) && (e || f && (g || i))",
                Description = "test min-terms generation",
                RuleName = "MinTermTest"
            };

            RuleGroup group = new()
            {
                RuleSet = new() { rule },
                GroupName = "default"
            };

            RuleEngineSettings engineSettings = new()
            {
                RuleGroups = new() { group }
            };

            using RuleEngine engine = new(engineSettings);
            // engine.GetProperty<RuleEngine, Dictionary<string, RuleGroupCompiled>>("test");
            var ruleGroups = engine.GetField<RuleEngine, Dictionary<string, RuleGroupCompiled>>("_ruleGroups");
            RuleSettingsCompiled ruleCompiled = ruleGroups["default"].RuleSet[0];
            var minTerms = ruleCompiled.MinTerms;

            // (a * e) + (a * f * g) + (a * f * i) + (b * e) + (b * f * g) + (b * f * i)
            List<string[]> expectedSequences = new() {
                new string[] { "a", "e" },
                new string[] { "a", "f", "g" },
                new string[] { "a", "f", "i" },
                new string[] { "b", "e" },
                new string[] { "b", "f", "g" },
                new string[] { "b", "f", "i" },
            };

            Assert.IsType<SimpleExpressionParataxis>(minTerms);
            SimpleExpressionParataxis minTermsParataxis = (SimpleExpressionParataxis)minTerms;
            // +
            Assert.Equal(SimpleExpressionBinaryOperator.Or, minTermsParataxis.Operator);
            Assert.Equal(expectedSequences.Count, minTermsParataxis.Operands.Count);
            foreach (ISimpleExpression minterm in minTermsParataxis.Operands)
            {
                Assert.IsType<SimpleExpressionParataxis>(minterm);
                SimpleExpressionParataxis mintermParataxis = (SimpleExpressionParataxis)minterm;
                // *
                Assert.Equal(SimpleExpressionBinaryOperator.And, mintermParataxis.Operator);

                string[] parameters = mintermParataxis.Operands.Select(xxxx => ((SimpleExpressionParameter)xxxx).ParameterName).ToArray();
                bool isMatchAny = false;
                for (var i = 0; i < expectedSequences.Count; i++)
                {
                    var expectedSequence = expectedSequences[i];
                    isMatchAny = expectedSequence.OrderBy(x => x).SequenceEqual(parameters.OrderBy(x => x));
                    if (isMatchAny)
                    {
                        expectedSequences.RemoveAt(i);
                        break;
                    }
                }
                Assert.True(isMatchAny);
            }
        }
    }
}

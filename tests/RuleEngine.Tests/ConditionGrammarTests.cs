using RuleEngine.Exceptions;
using RuleEngine.Models.RuleEngine;
using Xunit;

namespace RuleEngine.Tests
{
    public class ConditionGrammarTests
    {
        [Fact]
        public void TestFaultyGrammar()
        {
            RuleSettings rule = new()
            {
                ConditionExpression = "a >>>>>>> b",
                Description = "Wrong grammar apparently",
                RuleName = "TestFaultyGrammar"
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

            bool hasException = false;

            try
            {
                RuleEngine engine = new(engineSettings);
            }
            catch (RuleEngineParseException ex)
            {
                hasException = true;
                Assert.Single(ex.Details.RuleGroups);
                Assert.Equal(2, ex.Details.RuleGroups[0].Rules[0].CharPositionInLine);
            }
            Assert.True(hasException);
        }
    }
}

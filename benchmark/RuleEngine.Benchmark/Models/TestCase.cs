using System.Collections.Generic;

namespace RuleEngine.Benchmark.Models
{
    public enum TestType
    {
        // first group
        AllRulesAreTheSame,
        NoRuleIsTheSame,
        // second group
        ManySameSubtrees,
        FewSameSubtrees
    }

    public class TestCase
    {
        public TestType Type { get; set; }
        public int RuleCount { get; set; }
        public int InputCount { get; set; }
        public RuleEngine Engine { get; set; }
        public Dictionary<string, object> Input { get; set; }
    }
}

using System.Collections.Generic;
namespace RuleEngine.Models.RuleEngine
{
    public class RuleSettings
    {
        public string RuleName { get; set; }
        public string ConditionExpression { get; set; }
        public int LogThresholdForTimes { get; set; }
        public string Description { get; set; }
    }
}

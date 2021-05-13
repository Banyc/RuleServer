using System.Collections.Generic;
namespace RuleEngine.Models.RuleService
{
    public class RuleSettings
    {
        public string RuleName { get; set; }
        public string ConditionExpression { get; set; }
        public int LogThresholdForTimes { get; set; }
    }
}

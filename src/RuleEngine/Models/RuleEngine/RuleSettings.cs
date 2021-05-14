using System;
using System.Collections.Generic;
namespace RuleEngine.Models.RuleEngine
{
    public class RuleSettings : ICloneable
    {
        public string RuleName { get; set; }
        public string ConditionExpression { get; set; }
        public int LogThresholdForTimes { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            return new RuleSettings()
            {
                ConditionExpression = this.ConditionExpression,
                Description = this.Description,
                LogThresholdForTimes = this.LogThresholdForTimes,
                RuleName = this.RuleName,
            };
        }
    }
}

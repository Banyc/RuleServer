using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using RuleServer.Models.Expression;

namespace RuleServer.Models.RuleService
{
    public class RuleSettingsCompiled : RuleSettings
    {
        private int _hitCount = 0;

        public ISimpleExpression ExpressionTree { get; set; }
        public ISimpleExpression MinTerms { get; set; }
        // public event Action<RuleSettingsCompiled> OnAlert;
        public int HitCount
        {
            get
            {
                lock (this)
                {
                    return _hitCount;
                }
            }
        }

        public RuleSettingsCompiled(RuleSettings ruleSettings)
        {
            this.ConditionExpression = ruleSettings.ConditionExpression;
            this.LogThresholdForTimes = ruleSettings.LogThresholdForTimes;
            this.RuleName = ruleSettings.RuleName;
        }

        public void IncrementHitCount()
        {
            lock (this)
            {
                // Increment HitCount
                _hitCount = (_hitCount + 1) % this.LogThresholdForTimes;
            }
        }
    }
}

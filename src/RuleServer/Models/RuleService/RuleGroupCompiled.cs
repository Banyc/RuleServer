using System.Collections.Generic;

namespace RuleServer.Models.RuleService
{
    public class RuleGroupCompiled : RuleGroup
    {
        public new List<RuleSettingsCompiled> RuleSet { get; set; }
    }
}

using System.Collections.Generic;
using RuleServer.Models.Expression;

namespace RuleServer.Models.RuleService
{
    public class RuleGroupCompiled : RuleGroup
    {
        public HashSet<ISimpleExpression> DuplicatedSubtrees { get; set; }
        public new List<RuleSettingsCompiled> RuleSet { get; set; }
        public Dictionary<string, RuleIndex> Index { get; set; } = new();
    }
}

using System.Collections.Generic;

namespace RuleServer.Models.RuleService
{
    public class RuleGroup
    {
        public string GroupName { get; set; }
        public List<RuleSettings> RuleSet { get; set; }
    }
}

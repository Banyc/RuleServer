using System.Collections.Generic;

namespace RuleEngine.Models.RuleEngine
{
    public class RuleGroup
    {
        public string GroupName { get; set; }
        public List<RuleSettings> RuleSet { get; set; }
        public List<string> IndexedParameters { get; set; }
    }
}

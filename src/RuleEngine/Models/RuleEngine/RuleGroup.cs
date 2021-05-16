using System.Collections.Generic;

namespace RuleEngine.Models.RuleEngine
{
    public class RuleGroup
    {
        public string GroupName { get; set; }
        public List<RuleSettings> RuleSet { get; set; } = new();
        public List<string> IndexedParameters { get; set; } = new();
        public int MaxIndexCacheSize { get; set; } = 1024;
    }
}

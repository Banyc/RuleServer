using System.Collections.Generic;
namespace RuleServer.Models.RuleService
{
    public class RuleServiceSettings
    {
        public List<RuleGroup> RuleGroups { get; set; }
        public string ServerName { get; set; }
    }
}

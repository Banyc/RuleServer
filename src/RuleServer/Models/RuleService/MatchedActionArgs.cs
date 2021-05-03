using System.Collections.Generic;

namespace RuleServer.Models.RuleService
{
    public class MatchedActionArgs
    {
        public RuleSettingsCompiled Rule { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}

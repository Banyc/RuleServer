using System;
using System.Collections.Generic;

namespace RuleEngine.Models.RuleEngine
{
    public class MatchedActionArgs
    {
        public RuleGroupCompiled Group { get; set; }
        public RuleSettingsCompiled Rule { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }

    public class ExceptionActionArgs
    {
        public RuleGroupCompiled Group { get; set; }
        public RuleSettingsCompiled Rule { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
        public Exception Exception { get; set; }
    }
}

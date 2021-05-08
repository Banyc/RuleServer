using System.Collections.Generic;
namespace RuleServer.Models.RuleService
{
    public class RuleIndex
    {
        public string ParameterName { get; set; }
        // key: argument
        // value: relative rules
        public Dictionary<object, List<RuleSettingsCompiled>> Index { get; set; }
    }
}

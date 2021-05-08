using System.Collections.Generic;
namespace RuleServer.Models.RuleService
{
    public class RuleIndex
    {
        public string ParameterName { get; set; }
        // key: argument
        // value: relative rules
        public Dictionary<object, HashSet<RuleSettingsCompiled>> IndexByValue { get; set; } = new();
        public HashSet<RuleSettingsCompiled> UncertainRules { get; set; } = new();

        public void AddToUncertainRules(RuleSettingsCompiled rule)
        {
            foreach (var kv in this.IndexByValue)
            {
                if (kv.Value.Contains(rule))
                {
                    kv.Value.Remove(rule);
                }
            }
            this.UncertainRules.Add(rule);
        }

        public void AddToIndexByValue(object value, RuleSettingsCompiled rule)
        {
            if (this.UncertainRules.Contains(rule))
            {
                return;
            }
            if (!this.IndexByValue.ContainsKey(value))
            {
                this.IndexByValue[value] = new();
            }
            this.IndexByValue[value].Add(rule);
        }

        // when in rule searching, user can only search for `IndexByValue` w/o unioning it with `UncertainRules`
        public void MergeUncertainRulesToIndexByValue()
        {
            foreach (var kv in this.IndexByValue)
            {
                kv.Value.UnionWith(this.UncertainRules);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using RuleEngine.Helpers;
using RuleEngine.Helpers.ExpressionParser;
using RuleEngine.Models.Expression;
using RuleEngine.Models.RuleEngine;

namespace RuleEngine
{
    public partial class RuleEngine
    {
        public void UpdateSettings(RuleEngineSettings settings)
        {
            _settings = settings;
            _logger?.LogInformation("Updating settings...");
            Dictionary<string, RuleGroupCompiled> newRuleGroups = new();
            foreach (var group in settings.RuleGroups)
            {
                List<RuleSettingsCompiled> newRuleSet = new();
                foreach (var rule in group.RuleSet)
                {
                    var ruleCompiled = RuleEngineLoadRulesStaticHelpers.ParseRule(rule);
                    newRuleSet.Add(ruleCompiled);
                }
                // optimize
                var duplicatedSubtree = RuleEngineLoadRulesStaticHelpers.Optimize(newRuleSet);
                // make new group
                RuleGroupCompiled newGroup = new()
                {
                    GroupName = group.GroupName,
                    RuleSet = newRuleSet,
                    DuplicatedSubtrees = duplicatedSubtree,
                    IndexedParameters = group.IndexedParameters,
                };
                // update index
                RuleEngineLoadRulesStaticHelpers.UpdateIndex(newGroup);
                // add new rules to rule groups
                newRuleGroups[group.GroupName] = newGroup;
            }
            // switch
            lock (this)
            {
                _ruleGroups = newRuleGroups;
            }

            _logger?.LogInformation("Done updating settings.");
        }
    }
}

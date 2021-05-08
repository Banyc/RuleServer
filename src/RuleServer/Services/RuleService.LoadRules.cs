using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.Logging;
using RuleServer.Helpers;
using RuleServer.Helpers.ExpressionParser;
using RuleServer.Models.Expression;
using RuleServer.Models.RuleService;

namespace RuleServer.Services
{
    public partial class RuleService
    {
        private void UpdateSettings(RuleServiceSettings settings)
        {
            _settings = settings;
            _logger.LogInformation("Updating settings...");
            Dictionary<string, RuleGroupCompiled> newRuleGroups = new();
            foreach (var group in settings.RuleGroups)
            {
                List<RuleSettingsCompiled> newRuleSet = new();
                foreach (var rule in group.RuleSet)
                {
                    var ruleCompiled = RuleServiceLoadRulesStaticHelpers.ParseRule(rule);
                    newRuleSet.Add(ruleCompiled);
                }
                // optimize
                var duplicatedSubtree = RuleServiceLoadRulesStaticHelpers.Optimize(newRuleSet);
                // make new group
                RuleGroupCompiled newGroup = new()
                {
                    GroupName = group.GroupName,
                    RuleSet = newRuleSet,
                    DuplicatedSubtrees = duplicatedSubtree,
                    IndexedParameters = group.IndexedParameters,
                };
                // update index
                RuleServiceLoadRulesStaticHelpers.UpdateIndex(newGroup);
                // add new rules to rule groups
                newRuleGroups[group.GroupName] = newGroup;
            }
            // switch
            lock (this)
            {
                _ruleGroups = newRuleGroups;
            }

            _logger.LogInformation("Done updating settings.");
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using RuleEngine.Exceptions;
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
            RuleGroupsParseErrorArgs ruleGroupsParseErrorArgs = new();
            foreach (var group in settings.RuleGroups)
            {
                List<RuleSettingsCompiled> newRuleSet = new();
                RuleGroupParseErrorArgs ruleGroupParseErrorArgs = new()
                {
                    GroupName = group.GroupName
                };
                // parse rules in the group
                foreach (var rule in group.RuleSet)
                {
                    try
                    {
                        var ruleCompiled = RuleEngineLoadRulesStaticHelpers.ParseRule(rule);
                        newRuleSet.Add(ruleCompiled);
                    }
                    catch (ParseErrorException ex)
                    {
                        // catch grammar error in rule.ConditionExpression
                        ruleGroupParseErrorArgs.Rules.Add(new()
                        {
                            CharPositionInLine = ex.CharPositionInLine,
                            Line = ex.Line,
                            ListIndex = group.RuleSet.IndexOf(rule),
                            Message = ex.Message,
                            RuleName = rule.RuleName,
                        });
                    }
                }
                // stop processing when grammar errors are found {
                if (ruleGroupParseErrorArgs.Rules.Count > 0)
                {
                    ruleGroupsParseErrorArgs.RuleGroups.Add(ruleGroupParseErrorArgs);
                }
                if (ruleGroupsParseErrorArgs.RuleGroups.Count > 0)
                {
                    // grammar errors are found.
                    // cancel the following operations
                    continue;
                }
                // }
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
            if (ruleGroupsParseErrorArgs.RuleGroups.Count > 0)
            {
                // throw grammatical exception
                _logger?.LogInformation("One or more grammar errors are found");
                throw new RuleEngineParseException("One or more grammar errors are found")
                {
                    Details = ruleGroupsParseErrorArgs
                };
            }
            else
            {
                // switch
                lock (this)
                {
                    _ruleGroups = newRuleGroups;
                }
                _logger?.LogInformation("Done updating settings.");
            }
        }
    }
}

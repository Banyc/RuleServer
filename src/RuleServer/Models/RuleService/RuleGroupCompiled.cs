using System;
using System.Collections.Generic;
using System.Linq;
using RuleServer.Helpers;
using RuleServer.Models.Expression;

namespace RuleServer.Models.RuleService
{
    public class RuleGroupCompiled : RuleGroup
    {
        public HashSet<ISimpleExpression> DuplicatedSubtrees { get; set; }
        public new List<RuleSettingsCompiled> RuleSet { get; set; }
        public Dictionary<string, RuleIndex> IndexByParameterName { get; set; } = new();
        // public new HashSet<string> IndexedParameters { get; set; }
        public Dictionary<List<object>, IEnumerable<RuleSettingsCompiled>> CachedIndex { get; set; } =
            new(new ListEqualityComparer<object>());

        public IEnumerable<RuleSettingsCompiled> GetMatchedRules(
            IDictionary<string, object> symbolTable)
        {
            IEnumerable<RuleSettingsCompiled> cachedIndexedRules =
                GetRulesFromCachedIndex(symbolTable);
            if (cachedIndexedRules != null)
            {
                return cachedIndexedRules;
            }

            HashSet<RuleSettingsCompiled> freshlySearchedRules = null;

            if (this.IndexedParameters != null)
            {
                foreach (string indexedParameterName in this.IndexedParameters)
                {
                    if (!symbolTable.ContainsKey(indexedParameterName))
                    {
                        continue;
                    }
                    object indexedArgument = symbolTable[indexedParameterName];
                    HashSet<RuleSettingsCompiled> matchedRules;
                    if (this.IndexByParameterName[indexedParameterName]
                            .IndexByValue.ContainsKey(indexedArgument))
                    {
                        matchedRules =
                            this.IndexByParameterName[indexedParameterName]
                                .IndexByValue[indexedArgument];
                    }
                    else
                    {
                        matchedRules =
                            this.IndexByParameterName[indexedParameterName]
                                .UncertainRules;
                    }
                    if (freshlySearchedRules == null)
                    {
                        freshlySearchedRules = matchedRules;
                    }
                    else
                    {
                        freshlySearchedRules.IntersectWith(matchedRules);
                    }
                }
            }

            // if no rule can be found, return the whole rule set.
            IEnumerable<RuleSettingsCompiled> matchedRule =
                freshlySearchedRules ?? (IEnumerable<RuleSettingsCompiled>)this.RuleSet;
            // cache the matched rules
            SetRulesToCachedIndex(symbolTable, matchedRule);
            return matchedRule;
        }

        public IEnumerable<RuleSettingsCompiled> GetRulesFromCachedIndex(
            IDictionary<string, object> symbolTable)
        {
            List<object> indexedArguments = GetIndexedArguments(symbolTable);
            if (this.CachedIndex.ContainsKey(indexedArguments))
            {
                return this.CachedIndex[indexedArguments];
            }
            else
            {
                return null;
            }
        }

        public void SetRulesToCachedIndex(
            IDictionary<string, object> symbolTable,
            IEnumerable<RuleSettingsCompiled> matchedRules)
        {
            List<object> indexedArguments = GetIndexedArguments(symbolTable);
            this.CachedIndex[indexedArguments] = matchedRules;
        }

        private List<object> GetIndexedArguments(IDictionary<string, object> symbolTable)
        {
            List<object> indexedArguments = new();
            if (this.IndexedParameters != null)
            {
                foreach (var indexedParameter in this.IndexedParameters)
                {
                    if (symbolTable.ContainsKey(indexedParameter))
                    {
                        indexedArguments.Add(symbolTable[indexedParameter]);
                    }
                    else
                    {
                        indexedArguments.Add(null);
                    }
                }
            }
            return indexedArguments;
        }
    }
}

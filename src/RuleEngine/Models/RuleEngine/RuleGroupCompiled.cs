using System;
using System.Collections.Generic;
using System.Linq;
using RuleEngine.Helpers;
using RuleEngine.Models.Expression;

namespace RuleEngine.Models.RuleEngine
{
    public class RuleGroupCompiled : RuleGroup
    {
        public HashSet<ISimpleExpression> DuplicatedSubtrees { get; set; }
        public new List<RuleSettingsCompiled> RuleSet { get; set; }
        public Dictionary<string, RuleIndex> IndexByParameterName { get; set; } = new();
        // public new HashSet<string> IndexedParameters { get; set; }
        public ConcurrentLimitedSizeDictionary<List<object>, IEnumerable<RuleSettingsCompiled>> CachedIndex { get; set; }
        public new int MaxIndexCacheSize { get => base.MaxIndexCacheSize; }

        public RuleGroupCompiled()
        {
            this.CachedIndex = new(this.MaxIndexCacheSize, new ListEqualityComparer<object>());
        }

        public IEnumerable<RuleSettingsCompiled> GetMatchedRules(
            IDictionary<string, object> symbolTable)
        {
            IEnumerable<RuleSettingsCompiled> cachedIndexedRules =
                GetRulesFromCachedIndex(symbolTable);
            if (cachedIndexedRules != null)
            {
                return cachedIndexedRules;
            }

            IEnumerable<RuleSettingsCompiled> matchedRules;
            if (this.IndexedParameters == null)
            {
                // user does not define index =>
                //   go check all rules
                matchedRules = this.RuleSet;
            }
            else
            {
                HashSet<RuleSettingsCompiled> freshlySearchedRules = null;
                foreach (string indexedParameterName in this.IndexedParameters)
                {
                    HashSet<RuleSettingsCompiled> tempMatchedRules;
                    if (!symbolTable.ContainsKey(indexedParameterName))
                    {
                        tempMatchedRules =
                            this.IndexByParameterName[indexedParameterName]
                                .UncertainRules;
                    }
                    else
                    {
                        object indexedArgument = symbolTable[indexedParameterName];
                        if (this.IndexByParameterName[indexedParameterName]
                                .IndexByValue.ContainsKey(indexedArgument))
                        {
                            tempMatchedRules =
                                this.IndexByParameterName[indexedParameterName]
                                    .IndexByValue[indexedArgument];
                        }
                        else
                        {
                            tempMatchedRules =
                                this.IndexByParameterName[indexedParameterName]
                                    .UncertainRules;
                        }
                    }
                    if (freshlySearchedRules == null)
                    {
                        freshlySearchedRules = tempMatchedRules;
                    }
                    else
                    {
                        freshlySearchedRules.IntersectWith(tempMatchedRules);
                    }
                }
                // if freshlySearchedRules is null,
                //   it is because:
                //     1. user defines an empty `IndexedParameters`
                // since `freshlySearchedRules` can also be considered as being initialized as universe,
                //   when `freshlySearchedRules` is null =>
                //     `freshlySearchedRules` is still in its initiation state =>
                //       this method should return universe
                if (freshlySearchedRules == null)
                {
                    matchedRules = this.RuleSet;
                }
                else
                {
                    matchedRules = freshlySearchedRules;
                }
            }

            // cache the matched rules
            SetRulesToCachedIndex(symbolTable, matchedRules);
            return matchedRules;
        }

        public IEnumerable<RuleSettingsCompiled> GetRulesFromCachedIndex(
            IDictionary<string, object> symbolTable)
        {
            List<object> indexedArguments = GetIndexedArguments(symbolTable);
            this.CachedIndex.TryGetValue(indexedArguments, out IEnumerable<RuleSettingsCompiled> value);
            return value;
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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RuleEngine.Helpers
{
    public class LruDictionary<TKey, TValue>
    {
        private readonly LruCache<TKey> keyLruCache;
        private readonly Dictionary<TKey, TValue> values;

        public int Count => this.values.Count;

        public TValue this[TKey key]
        {
            get
            {
                // get the value before modify `keyLruCache`
                var value = this.values[key];
                // update LRU
                this.keyLruCache.Refer(key);
                return value;
            }
            set
            {
                // remove the stale value
                var keyToRemove = this.keyLruCache.Refer(key);
                if (keyToRemove != null)
                {
                    this.values.Remove(keyToRemove);
                }
                this.values[key] = value;
            }
        }

        public LruDictionary(int capacity, IEqualityComparer<TKey> equalityComparer = null)
        {
            this.keyLruCache = new(capacity);
            if (equalityComparer == null)
            {
                this.values = new();
            }
            else
            {
                this.values = new(equalityComparer);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return this.values.ContainsKey(key);
        }
    }
}

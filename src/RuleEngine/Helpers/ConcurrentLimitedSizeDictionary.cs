using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RuleEngine.Helpers
{
    public class ConcurrentLimitedSizeDictionary<TKey, TValue>
    {
        public int ApproximateCapacity { get; }

        public int approximateCount = 0;

        private readonly ConcurrentDictionary<TKey, TValue> values;

        public TValue this[TKey key]
        {
            get
            {
                var value = this.values[key];
                return value;
            }
            set
            {
                if (!this.values.ContainsKey(key))
                {
                    if (this.approximateCount < this.ApproximateCapacity)
                    {
                        this.approximateCount++;
                        this.values[key] = value;
                    }
                    else
                    {
                        // storage is full. Don't set
                    }
                }
                else
                {
                    this.values[key] = value;
                }
            }
        }

        public ConcurrentLimitedSizeDictionary(int approximateCapacity, IEqualityComparer<TKey> equalityComparer = null)
        {
            this.ApproximateCapacity = approximateCapacity;
            this.values = new(equalityComparer);
        }

        public bool ContainsKey(TKey key)
        {
            return this.values.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.values.TryGetValue(key, out value);
        }
    }
}

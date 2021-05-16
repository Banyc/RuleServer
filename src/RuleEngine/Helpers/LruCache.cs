using System.Collections.Generic;
namespace RuleEngine.Helpers
{
    public class LruCache<TKey>
    {
        // first: most recently used;
        // last: least recently used.
        private readonly LinkedList<TKey> orderedKeys = new();
        private readonly int capacity;
        private readonly Dictionary<TKey, LinkedListNode<TKey>> linkedListIndex = new();

        public LruCache(int capacity)
        {
            this.capacity = capacity;
        }

        // return the key to be removed
        // return default if no key is to be removed
        public TKey Refer(TKey key)
        {
            TKey keyToRemove = default;
            // remove key from this.orderedKeys {
            if (this.linkedListIndex.ContainsKey(key))
            {
                // cache has record of the key
                this.orderedKeys.Remove(this.linkedListIndex[key]);
                // don't need <= the key will be added to this.orderedKeys later
                // this.linkedListIndex.Remove(key);
            }
            else
            {
                // cache has no record of the key
                // check if need to replace
                if (this.orderedKeys.Count == this.capacity)
                {
                    // the cache has no room for new comer
                    // must spare a space
                    var lastKey = this.orderedKeys.Last.Value;
                    keyToRemove = lastKey;
                    this.linkedListIndex.Remove(lastKey);
                    this.orderedKeys.RemoveLast();
                }
            }
            // }

            // prepend key to this.orderedKeys
            this.orderedKeys.AddFirst(key);
            this.linkedListIndex[key] = this.orderedKeys.First;

            // return the key to be removed
            return keyToRemove;
        }

        public bool IsInCache(TKey key)
        {
            return this.linkedListIndex.ContainsKey(key);
        }
    }
}

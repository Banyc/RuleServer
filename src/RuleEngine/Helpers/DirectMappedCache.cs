using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace RuleEngine.Helpers
{
    public class DirectMappedCache<TKey, TValue> : IDisposable
    {
        private KeyValuePair<TKey, TValue>[] rows;
        private readonly ReaderWriterLockSlim readerWriterLock = new();
        private readonly IEqualityComparer<TKey> equalityComparer;

#region public methods
        public DirectMappedCache(int rowSize, IEqualityComparer<TKey> equalityComparer = null)
        {
            this.rows = new KeyValuePair<TKey, TValue>[rowSize];
            this.equalityComparer = equalityComparer ?? EqualityComparer<TKey>.Default;
        }

        public TValue this[TKey key]
        {
            get
            {
                bool isFound = TryGetValue(key, out TValue value);
                if (isFound)
                {
                    return value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            set
            {
                int rowIndex = GetIndex(key);
                this.readerWriterLock.EnterWriteLock();
                this.rows[rowIndex] = new(key, value);
                this.readerWriterLock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            this.readerWriterLock.EnterWriteLock();
            this.rows = new KeyValuePair<TKey, TValue>[this.rows.Length];
            this.readerWriterLock.ExitWriteLock();
        }

        public bool ContainsKey(TKey key)
        {
            bool result;
            int rowIndex = GetIndex(key);
            this.readerWriterLock.EnterReadLock();
            result = ContainsKeyThreadUnsafe(key, rowIndex);
            this.readerWriterLock.ExitReadLock();
            return result;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            bool isFound;
            int rowIndex = GetIndex(key);
            this.readerWriterLock.EnterReadLock();
            if (ContainsKeyThreadUnsafe(key, rowIndex))
            {
                isFound = true;
                value = this.rows[rowIndex].Value;
            }
            else
            {
                isFound = false;
                value = default;
            }
            this.readerWriterLock.ExitReadLock();
            return isFound;
        }

        public void Dispose()
        {
            this.readerWriterLock.Dispose();
        }
#endregion // public methods

#region private methods
        private bool ContainsKeyThreadUnsafe(TKey key)
        {
            var rowIndex = GetIndex(key);
            return ContainsKeyThreadUnsafe(key, rowIndex);
        }
        private bool ContainsKeyThreadUnsafe(TKey key, int rowIndex)
        {
            var matchedKeyValuePair = this.rows[rowIndex];
            if (matchedKeyValuePair.Equals(default(KeyValuePair<TKey, TValue>)))
            {
                // KeyValuePair is not initialized
                return false;
            }
            else
            {
                bool isSameKey = this.equalityComparer.Equals(key, matchedKeyValuePair.Key);
                return isSameKey;
            }
        }

        private int GetIndex(TKey key)
        {
            int keyHash = this.equalityComparer.GetHashCode(key);
            int index = keyHash % this.rows.Length;
            if (index < 0)
            {
                index = -index;
            }
            return index;
        }
        #endregion  // private methods
    }
}

using System;
using RuleEngine.Helpers;
using Xunit;

namespace RuleEngine.Tests
{
    public class LruCacheTests
    {
        [Fact]
        public void Test1()
        {
            LruCache<int> lruCache = new(4);
            lruCache.Refer(1);
            lruCache.Refer(2);
            lruCache.Refer(3);
            lruCache.Refer(1);
            lruCache.Refer(4);
            int keyToRemove = lruCache.Refer(5);
            Assert.Equal(2, keyToRemove);
        }
    }
}

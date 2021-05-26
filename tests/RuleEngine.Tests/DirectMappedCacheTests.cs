using System.Threading.Tasks;
using System.Collections.Generic;
using RuleEngine.Helpers;
using Xunit;

namespace RuleEngine.Tests
{
    public class DirectMappedCacheTests
    {
        [Fact]
        public void SerialTests()
        {
            DirectMappedCache<int, int> cache = new(1);
            bool isKeyNotFound = false;
            try
            {
                var tmp = cache[3];
            }
            catch (KeyNotFoundException)
            {
                isKeyNotFound = true;
            }
            Assert.True(isKeyNotFound);
            cache[3] = 3;
            Assert.Equal(3, cache[3]);
            cache[3] = 33;
            Assert.Equal(33, cache[3]);
            cache[4] = 4;
            Assert.Equal(4, cache[4]);
            Assert.False(cache.TryGetValue(3, out _));
            cache.Dispose();
        }

        [Fact]
        public void ParallelTests()
        {
            DirectMappedCache<int, int> cache = new(1);
            const int parallelSize = 10000;
            List<Task> tasks = new();
            for (int i = 0; i < parallelSize; i++)
            {
                int key = i;
                Task task = Task.Run(() =>
                {
                    cache[key] = key;
                    var isFound = cache.TryGetValue(key, out var value);
                    if (isFound)
                    {
                        Assert.Equal(key, value);
                    }
                    else
                    {
                        Assert.Equal(default, value);
                    }
                });
                tasks.Add(task);
            }
            Task.WhenAll(tasks).Wait();
            cache.Dispose();
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace DeveloperSample.Syncing
{
    public class SyncTest
    {
        /**
         * Bun Thai's Note
         * Program end before task finished
         */
        [Fact]
        public void CanInitializeCollection()
        {
            var debug = new SyncDebug();
            var items = new List<string> { "one", "two" };
            var result = debug.InitializeList(items);
            Assert.Equal(items.Count, result.Count);
        }


        /**
         * Bun Thai's Note
         * Try to fix 3 threads processes not over 100 items
         * using ConcorrentQueue to check if the item already process
         */
        [Fact]
        public void ItemsOnlyInitializeOnce()
        {
            var debug = new SyncDebug();
            var count = 0;
            var dictionary = debug.InitializeDictionary(i =>
            {
                Thread.Sleep(1);
                Interlocked.Increment(ref count);
                return i.ToString();
            });

            Assert.Equal(100, count);
            Assert.Equal(100, dictionary.Count);
        }
    }
}
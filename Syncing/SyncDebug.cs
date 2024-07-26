using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new BlockingCollection<string>();
            Parallel.ForEach(items, async i =>
            {
                var r = await Task.FromResult(() => i).ConfigureAwait(false);
                bag.Add(r.Invoke());
            });
            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var itemQueue = new ConcurrentQueue<int>(itemsToInitialize);

            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            var threads = Enumerable.Range(0, 1)
                .Select(i => new Thread(() =>
                {
                    // Process Item only 1 time by a thread
                    while (itemQueue.TryDequeue(out var item))
                    {
                        concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                    }
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }


            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
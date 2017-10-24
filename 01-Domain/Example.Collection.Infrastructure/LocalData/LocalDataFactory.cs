using System;
using System.Collections.Concurrent;

namespace Example.Collection.Infrastructure
{
    public static class LocalDataFactory<TKey, TValue>
    {
        private static readonly ConcurrentDictionary<string, Lazy<ILocalData<TKey, TValue>>> regions = new ConcurrentDictionary<string, Lazy<ILocalData<TKey, TValue>>>();

        public static ILocalData<TKey, TValue> GetOrAdd(string regionName)
        {
            var lazy = regions.GetOrAdd(regionName, s => new Lazy<ILocalData<TKey, TValue>>(() => new LocalData<TKey, TValue>(s)));

            return lazy.Value;
        }
    }
}

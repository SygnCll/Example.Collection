using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Collection.Infrastructure
{
    public interface ILocalData<TKey, TValue>
    {
        string Region { get; }

        int Count { get; }

        bool ContainsKey(TKey key);

        TValue this[TKey key] { get; set; }

        bool TryGetValue(TKey key, out TValue value);

        TValue GetLastValue();

        void Clear();

        void Remove(TKey key);
    }
}

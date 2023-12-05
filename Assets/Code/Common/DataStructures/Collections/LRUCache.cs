using System;

namespace ChessMath.Shared.Common
{
    public class LRUCache<TKey, TValue> : Cache<TKey, TValue>
    {
        public LRUCache(int maxCapacity, int initialCapacity, Func<TKey, TValue> cachingFunction)
            : base(cachingFunction, new LRUDictionary<TKey, TValue>(maxCapacity, initialCapacity))
        {
        }

        public LRUCache(int maxCapacity, Func<TKey, TValue> cachingFunction) : base(cachingFunction, new LRUDictionary<TKey, TValue>(maxCapacity))
        {
        }
    }
}

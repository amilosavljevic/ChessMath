using System;
using System.Collections;
using System.Collections.Generic;

namespace ChessMath.Shared.Common
{
	/// Cache dictionary that lazy generates values from keys and cache them for later usage.
    public class Cache<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> values;
        private readonly Func<TKey, TValue> cachingFunction;

        public Cache(int initialCapacity, Func<TKey, TValue> cachingFunction) : this(cachingFunction, new Dictionary<TKey, TValue>(initialCapacity))
        {
        }

        public Cache(Func<TKey, TValue> cachingFunction) : this(cachingFunction, new Dictionary<TKey, TValue>())
        {
        }

        protected Cache(Func<TKey, TValue> cachingFunction, IDictionary<TKey, TValue> dictionary)
        {
            this.cachingFunction = cachingFunction;
            values = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

		public TValue Get(TKey key)
		{
			if (values.TryGetValue(key, out var value))
				return value;

			var newValue = cachingFunction(key);
			values.Add(key, newValue);
			return newValue;
		}

        public bool TryGetValue(TKey key, out TValue value) =>
            values.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable)values).GetEnumerator();

        public int Count => values.Count;

        public bool ContainsKey(TKey key) =>
            values.ContainsKey(key);

        public TValue this[TKey key] => Get(key);
        public IEnumerable<TKey> Keys => values.Keys;
        public IEnumerable<TValue> Values => values.Values;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChessMath.Shared.Common
{
    /// <summary>
    /// Dictionary that automatically removes last used key-value when to keep capacity at desired maximum size.
    /// </summary>
    public class LRUDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, Entry> dictionary;
        private readonly LinkedList<TKey> list;
        private readonly int maxCapacity;

        public LRUDictionary(int maxCapacity)
            : this(maxCapacity, new Dictionary<TKey, Entry>())
        {
        }

        public LRUDictionary(int maxCapacity, int initialCapacity)
            : this(maxCapacity, new Dictionary<TKey, Entry>(initialCapacity))
        {
        }

        private LRUDictionary(int maxCapacity, Dictionary<TKey, Entry> dictionary)
        {
            this.maxCapacity = maxCapacity;
            this.dictionary = dictionary;
            list = new LinkedList<TKey>();
        }

        public int Count => dictionary.Count;
        public bool IsReadOnly => false;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            list.Select(key => new KeyValuePair<TKey,TValue>(key, dictionary[key].Value))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public void Add(KeyValuePair<TKey, TValue> item) =>
            Add(item.Key, item.Value);

        public void Add(TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                var entry = dictionary[key];
                TrackNodeAsUsed(entry.Node);
                dictionary[key] = entry.WithValue(value);
                return;
            }

            if (dictionary.Count >= maxCapacity)
            {
                var keyToRemove = list.Last.Value;
                dictionary.Remove(keyToRemove);
                list.RemoveLast();
            }

            // add cache
            var newNode = list.AddFirst(key);
            dictionary.Add(key, new Entry(newNode, value));
        }

        private void TrackNodeAsUsed(LinkedListNode<TKey> node)
        {
            list.Remove(node);
            list.AddFirst(node);
        }

        public void Clear()
        {
            dictionary.Clear();
            list.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            dictionary.ContainsKey(item.Key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            throw new System.NotImplementedException();

        public bool Remove(KeyValuePair<TKey, TValue> item) =>
            dictionary.Remove(item.Key);

        public bool ContainsKey(TKey key) =>
            dictionary.ContainsKey(key);

        public bool Remove(TKey key)
        {
            if (!dictionary.TryGetValue(key, out var entry))
                return false;

            list.Remove(entry.Node);
            dictionary.Remove(entry.Key);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!dictionary.TryGetValue(key, out var entry))
            {
                value = default;
                return false;
            }

            value = entry.Value;
            TrackNodeAsUsed(entry.Node);
            return true;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!dictionary.TryGetValue(key, out var entry))
                    throw new KeyNotFoundException($"Key not found: {key}");

                TrackNodeAsUsed(entry.Node);
                return entry.Value;
            }
            set => Add(key, value);
        }

        public ICollection<TKey> Keys => dictionary.Keys;
        public ICollection<TValue> Values =>
            list
                .Select(k => dictionary[k].Value)
                .ToList();

        private readonly struct Entry
        {
            public readonly LinkedListNode<TKey> Node;
            public readonly TValue Value;
            public TKey Key => Node.Value;

            public Entry(LinkedListNode<TKey> node, TValue value)
            {
                Node = node;
                Value = value;
            }

            public Entry WithValue(TValue newValue) =>
                new Entry(Node, newValue);
        }
    }
}

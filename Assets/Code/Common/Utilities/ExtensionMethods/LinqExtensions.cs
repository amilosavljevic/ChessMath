using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common
{
    public static class LinqExtensions
	{
		public static bool AreAllElementsSame<T>(this IEnumerable<T> enumerable) =>
			enumerable.AreAllElementsSame(out var _);

		public static bool AreAllElementsSame<T>(this IEnumerable<T> enumerable, out T repeatingElement)
		{
			var index = -1;
			T firstElement = default;

			foreach (var element in enumerable)
			{
				index++;

				if (index == 0)
				{
					firstElement = element;
					continue;
				}

				if (!EqualityComparer<T>.Default.Equals(firstElement, element))
				{
					repeatingElement = default;
					return false;
				}
			}

			repeatingElement = firstElement;
			return true;
		}

#if !NET_STANDARD_2_1

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable) =>
            new HashSet<T>(enumerable);
#endif

		public static Dictionary<TKey, TValue> ToDictionary<TElement, TKey, TValue>(this IEnumerable<TElement> elements, Func<TElement, TKey> keySelector, Func<TElement, TValue> valueSelector)
		{
			var results = new Dictionary<TKey, TValue>();

			foreach (var element in elements)
			{
				var key = keySelector(element);

				if (!results.ContainsKey(key))
					results.Add(key, valueSelector(element));
			}

			return results;
		}

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> elements)
		{
			var results = new Dictionary<TKey, TValue>();

			foreach (var (key, value) in elements)
				results[key] = value;

			return results;
		}

		public static IEnumerable<T> Concat<T>(this IEnumerable<T> original, T element)
		{
			foreach (var value in original)
				yield return value;

			yield return element;
		}

        /// <summary>
        /// Returns first index of element if any, else it returns -1;
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> elements, T elementToFind)
		{
			var i = 0;

            var comparer = EqualityComparer<T>.Default;

			foreach (var element in elements)
			{
				if (comparer.Equals(element, elementToFind))
					return i;
				i++;
			}

			return -1;
		}

        /// <summary>
        /// Returns the index of element that matches criteria.
        /// </summary>
        public static int FindIndex<T>(this IEnumerable<T> elements, Predicate<T> predicate)
        {
            var i = 0;

            foreach (var element in elements)
            {
                if (predicate(element))
                    return i;
                i++;
            }

            return -1;
        }
	}
}

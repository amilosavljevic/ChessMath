using System;
using System.Collections.Generic;
using System.Collections;

namespace ChessMath.Shared.Common
{
	public static class ListExtensions
	{
        private static readonly Random random = new Random();

		/// <summary>
		/// Shuffle the list of elements.
		/// </summary>
		public static void Shuffle<T>(this IList<T> list) =>
			Shuffle(list, random);

		/// <summary>
		/// Shuffle the list of elements using external random provider.
		/// </summary>
		public static void Shuffle<T>(this IList<T> list, Random random)
		{
			var count = list.Count;

			for (var i = 0; i < count; i++)
			{
				var j = random.Next(count);

				// Swap
				(list[i], list[j]) = (list[j], list[i]);
			}
		}

		/// <summary>
		/// Removes the last element in list and returns it back. Throws when list is empty. Useful if you
		/// want to use list as a stack.
		/// </summary>
		public static T RemoveLast<T>(this IList<T> list)
		{
			if (list.Count == 0)
				throw new InvalidOperationException("Cannot remove element from empty list");

			var lastIndex = list.Count - 1;
			var element = list[lastIndex];
			list.RemoveAt(lastIndex);
			return element;
		}

		public static T RemoveLastIfAny<T>(this IList<T> list) =>
			list.Count > 0
				? list.RemoveLast()
				: default;

        //  Sorts an array in-place
        public static void Sort<T>(this T[] array, Comparison<T> comparison) =>
            Array.Sort(array, comparison);

        public static void SortBy<T, TOut>(this T[] array, Func<T, TOut> sortValueSelector)
        {
            Array.Sort(array, (l, r) => Comparer<TOut>.Default.Compare(sortValueSelector(l), sortValueSelector(r)));
        }

        public static void SortByDecending<T, TOut>(this T[] array, Func<T, TOut> sortValueSelector)
        {
            Array.Sort(array, (l, r) => -Comparer<TOut>.Default.Compare(sortValueSelector(l), sortValueSelector(r)));
        }

        public static void SortBy<T, TOut>(this List<T> list, Func<T, TOut> sortValueSelector)
        {
            list.Sort((l, r) => Comparer<TOut>.Default.Compare(sortValueSelector(l), sortValueSelector(r)));
        }
    }

    // Wraps a generic Comparison<T> delegate in an IComparer to make it easy
    // to use a lambda expression for methods that take an IComparer or IComparer<T>
    internal class ComparisonComparer<T> : IComparer<T>, IComparer
    {
        private readonly Comparison<T> comparison;

        public ComparisonComparer(Comparison<T> comparison) =>
            this.comparison = comparison;

        public int Compare(T x, T y) =>
            comparison(x, y);

        public int Compare(object o1, object o2) =>
            comparison((T)o1, (T)o2);
    }
}

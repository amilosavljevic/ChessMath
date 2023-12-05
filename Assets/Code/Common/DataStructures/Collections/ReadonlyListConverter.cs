using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChessMath.Shared.Common
{
    public class ReadOnlyListConverter<TOriginal, TConverted> : IReadOnlyList<TConverted>
    {
        private readonly IReadOnlyList<TOriginal> originalList;
        private readonly Converter<TOriginal, TConverted> converter;

        public ReadOnlyListConverter(IReadOnlyList<TOriginal> originalList, Converter<TOriginal, TConverted> converter)
        {
            this.originalList = originalList;
            this.converter = converter;
        }

        public IEnumerator<TConverted> GetEnumerator() =>
            originalList
                .Select( o => converter(o))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int Count =>
            originalList.Count;

        public TConverted this[int index] =>
            converter(originalList[index]);
    }
}

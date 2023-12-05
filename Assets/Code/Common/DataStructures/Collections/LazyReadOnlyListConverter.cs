using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChessMath.Shared.Common
{
    public class LazyReadOnlyListConverter<TOriginal, TConverted> : IReadOnlyList<TConverted>
    {
        private readonly Func<IReadOnlyList<TOriginal>> listGetter;
        private readonly Converter<TOriginal, TConverted> converter;

        public LazyReadOnlyListConverter(Func<IReadOnlyList<TOriginal>> listGetter, Converter<TOriginal, TConverted> converter)
        {
            this.listGetter = listGetter;
            this.converter = converter;
        }

        private IReadOnlyList<TOriginal> OriginalList =>
            listGetter();

        public IEnumerator<TConverted> GetEnumerator() =>
            OriginalList
                .Select( o => converter(o))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int Count =>
            OriginalList.Count;

        public TConverted this[int index] =>
            converter(OriginalList[index]);
    }
}

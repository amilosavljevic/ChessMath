using System;
using System.Collections;
using ChessMath.Shared.Common;

namespace Solitaire.Common.Coroutines
{
    /// <summary>
    ///  Coroutine that will yield every X seconds (by default 1). You can use it when you are ticking something each
    /// second so you do not instantiate new "WaitForSeconds" every time.
    /// </summary>
    public class TickerCoroutine : IEnumerator
    {
        private readonly ITimeProvider timeProvider;
        private readonly TimeSpan tickDuration;
        private DateTime nextTickTime;

        public TickerCoroutine(ITimeProvider timeProvider) : this(timeProvider, TimeSpan.FromSeconds(1))
        {
        }

        public TickerCoroutine(ITimeProvider timeProvider, TimeSpan tickDuration)
        {
            this.timeProvider = timeProvider;
            this.tickDuration = tickDuration;
            nextTickTime = this.timeProvider.UtcNow + tickDuration;
        }

        public bool MoveNext()
        {
            var now = timeProvider.UtcNow;

            if (now < nextTickTime)
                return true;

            nextTickTime += tickDuration;
            return false;
        }

        public IEnumerator WaitOneTick() =>
            this;

        public void Reset() => throw new NotImplementedException();
        public object Current => null;
    }
}

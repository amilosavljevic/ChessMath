using System;

namespace Solitaire.Common.Coroutines
{
    public class WaitUntil : Coroutine
    {
        private readonly Func<bool> condition;

        public WaitUntil(Func<bool> condition) =>
            this.condition = condition;

        protected override void Update()
        {
            if (condition())
                Stop();
        }
    }
}
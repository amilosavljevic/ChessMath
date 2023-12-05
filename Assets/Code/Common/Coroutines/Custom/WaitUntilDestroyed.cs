using UnityEngine;

namespace Solitaire.Common.Coroutines
{
    public class WaitUntilDestroyed : Coroutine
    {
        private readonly Object target;

        public WaitUntilDestroyed(Object target) =>
            this.target = target;

        protected override void Update()
        {
            base.Update();
            if (target == null)
                Stop();
        }
    }
}
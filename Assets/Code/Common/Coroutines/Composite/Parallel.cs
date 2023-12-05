using System.Collections.Generic;

namespace Solitaire.Common.Coroutines
{
    public class Parallel : CompositeCoroutine
    {
        public Parallel()
        {
        }

        public Parallel(IEnumerable<ICoroutine> coroutines) =>
            this.Coroutines.AddRange(coroutines);

        protected sealed override void Update()
        {
            foreach (var coroutine in Coroutines)
                coroutine.MoveNext();

            if (AreAllDone())
                Stop();
        }

        private bool AreAllDone()
        {
            for (var index = 0; index < Coroutines.Count; index++)
            {
                if (!Coroutines[index].IsDone)
                    return false;
            }

            return true;
        }
    }
}
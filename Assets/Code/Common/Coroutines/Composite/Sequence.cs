namespace Solitaire.Common.Coroutines
{
    public class Sequence : CompositeCoroutine
    {
        private int currentIndex ;

        protected override void Update()
        {
            if (currentIndex >= Coroutines.Count)
            {
                Stop();
                return;
            }

            var coroutine = Coroutines[currentIndex];

            if (coroutine.MoveNext())
                Current = coroutine.Current;

            // if (!coroutine.IsDone && coroutine.Current is IEnumerator nestedEnumerator)
            // Current = nestedEnumerator;

            if (coroutine.IsDone) currentIndex++;

            if (currentIndex >= Coroutines.Count)
                Stop();
        }

    }
}

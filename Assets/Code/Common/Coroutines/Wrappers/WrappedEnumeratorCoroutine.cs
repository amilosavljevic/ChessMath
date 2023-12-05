using System.Collections;

namespace Solitaire.Common.Coroutines
{
	public class WrappedEnumeratorCoroutine : Coroutine
	{
        private readonly CoroutineRunner runner;

        public WrappedEnumeratorCoroutine(IEnumerator enumerator) =>
            runner = new CoroutineRunner(enumerator);

        protected override void Update()
        {
            runner.Tick();
            Current = runner.LastYield;

            if (runner.IsDone)
                Stop();
        }
	}
}

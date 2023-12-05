using System.Collections.Generic;

namespace Solitaire.Common.Coroutines
{
	public class WaitForAny : CompositeCoroutine
	{
		public ICoroutine FinishedCoroutine { get; private set; }

        public WaitForAny() { }

        public WaitForAny (IEnumerable<ICoroutine> coroutines) =>
            this.Coroutines.AddRange(coroutines);

        protected sealed override void Update()
        {
            foreach (var coroutine in Coroutines)
                coroutine.MoveNext();
			
            CheckStopCondition();
        }
        
        protected void CheckStopCondition()
        {
            var firstDone = GetFirstDoneCoroutine();
            if (firstDone == null) return;

            FinishedCoroutine = firstDone;
            Stop();
        }

        private ICoroutine GetFirstDoneCoroutine()
		{
			for (var index = 0; index < Coroutines.Count; index++)
			{
				var coroutine = Coroutines[index]; 
				if (coroutine.IsDone)
					return coroutine;
			}

			return null;
		}
	}
}
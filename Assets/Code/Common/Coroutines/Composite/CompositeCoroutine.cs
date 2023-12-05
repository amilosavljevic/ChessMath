using System.Collections;
using System.Collections.Generic;

namespace Solitaire.Common.Coroutines
{
	public abstract class CompositeCoroutine : Coroutine, IReadOnlyList<ICoroutine>
	{
		protected readonly List<ICoroutine> Coroutines = new List<ICoroutine>();

		protected override void OnDestroy()
		{
			foreach (var nCoroutine in Coroutines)
				nCoroutine.Dispose();
		}

        public void Add (IEnumerator instruction)
        {
            if (instruction is ICoroutine coroutine) Coroutines.Add(coroutine);
            else Coroutines.Add(new WrappedEnumeratorCoroutine(instruction));
        }

        public int Count => Coroutines.Count;
		public ICoroutine this[int index] => Coroutines[index];

		public IEnumerator<ICoroutine> GetEnumerator() =>
			Coroutines.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();
	}
}

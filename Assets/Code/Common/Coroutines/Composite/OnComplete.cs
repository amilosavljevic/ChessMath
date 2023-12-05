using System;
using System.Collections;

namespace Solitaire.Common.Coroutines
{
    public static class WaitForAnyExtensions
    {
        public static Sequence OnDone(this Coroutine coroutine, Action action)
        {
            return new Sequence() {
                coroutine, ExecuteAction(action)
            };
        }

        private static IEnumerator ExecuteAction(Action action)
        {
            action?.Invoke();
            yield return null;
        }
    }
}

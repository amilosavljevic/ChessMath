using System.Collections;

namespace Solitaire.Common.Coroutines
{
    public static class OrCoroutineExtensions
    {
        public static WaitForAny Or(this WaitForAny waitForAny, IEnumerator enumerator)
        {
            waitForAny.Add(enumerator);
            return waitForAny;
        }
        
        public static WaitForAny Or(this IEnumerator first, IEnumerator second) =>
            new WaitForAny() { first, second };
    }
}
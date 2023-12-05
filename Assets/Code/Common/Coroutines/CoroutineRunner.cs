using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire.Common.Coroutines
{
    public class CoroutineRunner : IDisposable
    {
        private readonly Stack<IEnumerator> enumerators = new Stack<IEnumerator>();
        public object LastYield { get; private set; }
        public bool IsDone => enumerators.Count == 0;

        public CoroutineRunner(IEnumerator enumerator) =>
            enumerators.Push(enumerator);

        public void Tick()
        {
            if (enumerators.Count == 0)
                return;

            var depthCounter = 0;

            while (enumerators.Count > 0)
            {
                var enumerator = enumerators.Peek();
                try
                {
                    bool isDone;

                    /*if (enumerator is ITickableCoroutine coroutine)
                    {
                        coroutine.Tick(delta);
                        isDone = coroutine.IsDone;
                    }
                    else*/
                    {
                        isDone = !enumerator.MoveNext();
                    }

                    if (isDone)
                    {
                        // Enumerator is exhausted?
                        DiscardLast();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    Dispose();
                    return;
                }

                // Enumerator returned something!
                var result = enumerator.Current;

                // Returned new enumerator -> push it on stack, and restart!
                // We restart same tick (greedy strategy).
                if (result is IEnumerator e)
                {
                    enumerators.Push(e);
                    depthCounter++;

                    if (depthCounter > 100)
                    {
                        throw new Exception("Busy-wait detected in coroutine: " + e);
                    }

                    continue;
                }

                if (result is YieldInstruction)
                {
                    throw new NotImplementedException($"Yield instructions are now not supported. Type={result}; Enumerator=" + enumerators.Peek());
                }

                // Returned regular value?
                // Just keep track it and break (pause until next tick).
                LastYield = enumerator.Current;
                break;
            }

            if (enumerators.Count == 0)
                Dispose();
        }

        protected virtual void LogException(Exception ex) =>
            Debug.LogException(ex);

        private void DiscardLast()
        {
            if (enumerators.Count == 0)
                return;

            var enumerator = enumerators.Pop();

            if (!(enumerator is IDisposable disposable))
                return;

            try
            {
                disposable.Dispose();
            }
            catch (Exception e)
            {
                LogException(e);
            }
        }

        public void Dispose()
        {
            while (enumerators.Count > 0)
                DiscardLast();
        }
    }
}

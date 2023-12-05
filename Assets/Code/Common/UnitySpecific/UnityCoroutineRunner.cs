using System.Collections;
using ChessMath.Shared.Common;
using ChessMath.Shared.UnityCommon.Utils;
using UnityEngine;

namespace ChessMath.Shared.UnityCommon.Implementations
{
    public class UnityCoroutineRunner : ICoroutineRunner
    {
        public CoroutineHandle StartCoroutine(IEnumerator routine)
        {
            var coroutine = MonoBehaviourProxy.I.StartCoroutine(routine);
            return new CoroutineHandle(this, coroutine);
        }

        public void StopCoroutine(object context) =>
            MonoBehaviourProxy.I.StopCoroutine((Coroutine)context);
    }
}
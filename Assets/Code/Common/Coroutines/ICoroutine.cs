using System;
using System.Collections;

namespace Solitaire.Common.Coroutines
{
    public interface ICoroutine : IEnumerator, IDisposable
    {
        bool IsDone { get; }
    }
}
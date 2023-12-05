using System;

namespace ChessMath.Shared.Common
{
    public readonly struct CoroutineHandle : IDisposable
    {
        private readonly ICoroutineRunner runner;
        private readonly object context;

        public CoroutineHandle(ICoroutineRunner runner, object context)
        {
            this.runner = runner;
            this.context = context;
        }

        public void Stop() =>
            runner?.StopCoroutine(context);

        public void Dispose() =>
            Stop();
    }
}

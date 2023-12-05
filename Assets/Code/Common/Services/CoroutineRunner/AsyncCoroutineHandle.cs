using System;

namespace ChessMath.Shared.Common
{
    public class AsyncCoroutineHandle : IDisposable, IAsync
    {
        private readonly CoroutineHandle handle;
        private readonly Async async;

        public AsyncCoroutineHandle(CoroutineHandle handle, Async async)
        {
            this.handle = handle;
            this.async = async;
        }

        public void Stop()
        {
            handle.Stop();
            if (async.State == AsyncState.InProgress)
                async.Fail(); // Or Finish()?
        }

        public void Dispose() =>
            Stop();

        public object Context => async.Context;
        public event AsyncStateChangedDelegate StateChanged
        {
            add => async.StateChanged += value;
            remove => async.StateChanged -= value;
        }

        public AsyncState State => async.State;
    }
}

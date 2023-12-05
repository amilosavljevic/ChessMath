using System;

namespace ChessMath.Shared.Common
{
    public class LambdaDisposable : IDisposable
    {
        private bool isDisposed;
        private readonly Action disposeAction;

        public LambdaDisposable(Action disposeAction) =>
            this.disposeAction = disposeAction;

        public void Dispose()
        {
            if (isDisposed)
                return;
            isDisposed = true;
            disposeAction?.Invoke();
        }
    }
}

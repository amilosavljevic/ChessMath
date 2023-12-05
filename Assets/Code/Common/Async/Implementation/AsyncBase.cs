using System;

namespace ChessMath.Shared.Common
{
    public abstract class AsyncBase : IAsync
    {
        private AsyncStateChangedDelegate stateChanged;
        public object Context { get; private set; }
        public AsyncState State { get; private set; }

        private void SafeInvoke(AsyncStateChangedDelegate callback) =>
            callback?.Invoke(this);

        public event AsyncStateChangedDelegate StateChanged
        {
            add
            {
                if (IsDone)
                {
                    SafeInvoke(value);
                    return;
                }
                
                stateChanged += value;
            }
            remove => stateChanged -= value;
        }

        protected void SetState(AsyncState newState, object context = null)
        {
            if (State != AsyncState.InProgress)
                throw new InvalidOperationException("Cannot finish already finished task");
            
            State = newState;
            Context = context;
            SafeInvoke(stateChanged);
        }

        protected bool IsDone =>
            State != AsyncState.InProgress;
    }
}
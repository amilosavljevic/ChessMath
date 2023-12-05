using System;
using System.Collections;

namespace Solitaire.Common.Coroutines
{
    public abstract class Coroutine : ICoroutine
    {
        public event Action Done;
        private CoroutineState state;
        public object Current { get; protected set; }

        bool IEnumerator.MoveNext()
        {
            if (state == CoroutineState.New)
            {
                state = CoroutineState.Started;
                OnStart();
            }

            if (!IsDone)
                Update();

            return !IsDone;
        }
        
        public void Stop()
        {
            if (state == CoroutineState.Done)
                return;
            
            if (state == CoroutineState.Started)
                OnStop();
            
            state = CoroutineState.Done;
            Done?.Invoke();
            OnDestroy();
        }

        protected virtual void OnStart() { }
        protected virtual void OnStop() {}
        protected virtual void Update() { }
        protected virtual void OnDestroy() { }

        protected virtual void OnReset() =>
            throw new NotImplementedException();

        public void Reset()
        {
            if (state == CoroutineState.New)
                return;
			
            OnReset();
            state = CoroutineState.New;
        }

        public bool IsDone => state == CoroutineState.Done;
        
        void IDisposable.Dispose() => Stop();
    }
}
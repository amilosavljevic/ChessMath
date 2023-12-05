using System.Collections;

namespace ChessMath.Shared.Common
{
    public abstract class GameService : IGameService
    {
        protected IContext Context { get; }
        public bool IsInitialized { get; private set; }

        protected GameService (IContext context) =>
            Context = context;

        public IEnumerator Initialize()
        {
            OnInitializationStarted();
            yield return InitializationFlow();
            IsInitialized = true;
            OnInitializationFinished();
        }
        
        protected virtual void OnInitializationStarted() { }
        protected abstract IEnumerator InitializationFlow();
        protected virtual void OnInitializationFinished() { }
    }
}
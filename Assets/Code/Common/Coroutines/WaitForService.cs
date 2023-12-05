using ChessMath.Shared.Common;

namespace Solitaire.Common.Coroutines
{
    public class WaitForService<T> : Coroutine
        where T:class
    {
        public T Service { get; private set; }
        
        protected override void Update()
        {
            base.Update();

            Service ??= GlobalContext.TryGetService<T>();

            if (IsInitialized)
                Stop();
        }

        public bool IsInitialized =>
            Service switch
            {
                null => false,
                IGameService gameService => gameService.IsInitialized,
                _ => true
            };
    }
}
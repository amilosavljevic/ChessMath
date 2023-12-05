using System;
using ChessMath.Shared.Common.EventBusNs;

namespace ChessMath.Shared.Common.AppContextNs
{
    public class GameContext : Context
    {
        protected GameContext() : this(new EventBus())
        {
        }

        protected GameContext(IEventBus eventBus)
            : base(eventBus)
        {
        }

        protected void RegisterService<T>(T service) where T : class =>
            Services.Register(service);

        protected void RegisterService(object service, Type type) =>
            Services.Register(service, type);
    }
}

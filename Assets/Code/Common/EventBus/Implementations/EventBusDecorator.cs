using System;

namespace ChessMath.Shared.Common.EventBusNs
{
	public abstract class EventBusDecorator : IEventBus
	{
        protected readonly IEventBus EventBus;

		protected EventBusDecorator(IEventBus eventBus) =>
			EventBus = eventBus;

        public virtual void Publish<T>(T message)
            where T : IEvent =>
                EventBus.Publish(message);

		public virtual void Subscribe<T>(Action<T> handler)
            where T : IEvent =>
			    EventBus.Subscribe(handler);

		public virtual void Unsubscribe<T>(Action<T> handler)
            where T : IEvent =>
			    EventBus.Unsubscribe(handler);
	}
}

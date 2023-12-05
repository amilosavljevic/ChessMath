using System;

namespace ChessMath.Shared.Common
{
	public delegate void AutoUnsubscribeDelegate();
	
	public static class EventBusExtensions
	{
		public static AutoUnsubscribeDelegate SubscribeAndGenerateUnsubscribe<TEvent>(this IEventBus eventBus, Action<TEvent> handler)
			where TEvent : IEvent
		{
			eventBus.Subscribe(handler);
			return () => eventBus.Unsubscribe(handler);
		}
	}
}
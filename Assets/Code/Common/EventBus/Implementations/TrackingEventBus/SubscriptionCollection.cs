using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.EventBusNs
{
	internal class SubscriptionCollection<TEvent> : HashSet<Action<TEvent>>, ISubscriptionCollection where TEvent : IEvent
	{
		private readonly IEventBus eventBus;

		public SubscriptionCollection(IEventBus eventBus) =>
			this.eventBus = eventBus;

		public void UnregisterAll()
		{
			foreach (var handler in this)
				eventBus.Unsubscribe(handler);

			Clear();
		}
	}
}

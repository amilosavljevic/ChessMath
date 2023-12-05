using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.EventBusNs
{
	public class TrackingEventBus : EventBusDecorator, IDisposable
	{
        // TODO: This implementation is not optimal, add override: IEventBus.Unsubscribe(Type, Delegate).
		private readonly Dictionary<Type, ISubscriptionCollection> subscriptions =
			new Dictionary<Type, ISubscriptionCollection>();

		public TrackingEventBus(IEventBus eventBus) : base(eventBus)
        {
        }

        public override void Subscribe<T>(Action<T> handler)
		{
			base.Subscribe(handler);
			TrackSubscription(handler);
		}

		private void TrackSubscription<T>(Action<T> handler) where T : IEvent
		{
			var type = typeof(T);

			if (!subscriptions.TryGetValue(type, out var subscriptionsForEvent))
			{
				subscriptionsForEvent = new SubscriptionCollection<T>(this);
				subscriptions.Add(type, subscriptionsForEvent);
			}

			var subs = (SubscriptionCollection<T>)subscriptionsForEvent;
			subs.Add(handler);
		}

		public void UnregisterAll()
		{
			foreach (var kv in subscriptions)
				kv.Value.UnregisterAll();

			subscriptions.Clear();
		}

		public void Dispose() => UnregisterAll();
    }

	public static class TrackingEventBusExtensions
	{
		public static TrackingEventBus WithSubscriptionTracking(this IEventBus bus) =>
			new TrackingEventBus(bus);
	}
}

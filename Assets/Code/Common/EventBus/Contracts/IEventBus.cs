using System;

namespace ChessMath.Shared.Common
{
	/// <summary>
	/// Simple event bus abstraction (Publish–subscribe pattern).
	/// </summary>
	public interface IEventBus
    {
        void Publish<T>(T message) where T : IEvent;

		void Subscribe<T>(Action<T> handler) where T : IEvent;
		void Unsubscribe<T>(Action<T> handler) where T : IEvent;
	}
}

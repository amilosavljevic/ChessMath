using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.EventBusNs
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> genericHandlers =
            new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> genericHandler)
            where T : IEvent
        {
            var type = typeof(T);
            if (HasHandlerFor(type))
            {
                var handler = (Action<T>)genericHandlers[type];
                handler -= genericHandler; // this prevents double registering
                handler += genericHandler;
                genericHandlers[type] = handler;
            }
            else
            {
                genericHandlers[type] = genericHandler;
            }
        }

        public void Unsubscribe<T>(Action<T> genericHandler)
            where T : IEvent
        {
            var type = typeof(T);
            if (!genericHandlers.TryGetValue(type, out var handlerRaw))
                return;

            var handler = (Action<T>)handlerRaw;
            handler -= genericHandler;
            genericHandlers[type] = handler;
        }

        public void Publish<T>(T value) where T : IEvent
        {
            var type = typeof(T);
            if (!genericHandlers.TryGetValue(type, out var handlerRaw))
                return;

            var handler = (Action<T>)handlerRaw;
            handler?.Invoke(value);
        }

        private readonly object[] parametersArray = new object[1];

        public void PublishAny(IEvent value)
        {
            var type = value.GetType();

            if (genericHandlers.TryGetValue(type, out var handler))
            {
                parametersArray[0] = value; 
                handler.Method.Invoke(handler.Target, parametersArray);
                parametersArray[0] = null;
            }
        }

        public bool HasHandlerFor<T>() =>
            HasHandlerFor(typeof(T));

        public bool HasHandlerFor(Type type) =>
            genericHandlers.ContainsKey(type);
    }
}

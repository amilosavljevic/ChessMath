using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.AppContextNs
{
    public sealed class ServiceCollection
    {
        private readonly Dictionary<Type, object> services =
            new Dictionary<Type, object>();

        public void Register<T>(T service) where T : class =>
            Register(service, typeof(T));

        public void Register(object service, Type type)
        {
            RegisterImpl(service, type);

            // Register actual type if different?
            var actualServiceType = service.GetType();
            
            if (type != actualServiceType)
                RegisterImpl(service, actualServiceType);
        }

        private void RegisterImpl(object service, Type type)
        {
            if (services == null) throw new ArgumentNullException(nameof(service));
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            if (services.TryGetValue(type, out var oldService) && !ReferenceEquals(oldService, service))
                throw new InvalidOperationException("Service already registered: " + type);

            services[type] = service;
        }
        
        public object GetService(Type type) =>
            services.TryGetValue(type, out var service)
                ? service
                : throw new InvalidOperationException($"Cannot find Service of type: {type}");

        public ICollection<object> GetAllServices() =>
            new HashSet<object>(services.Values);
    }
}

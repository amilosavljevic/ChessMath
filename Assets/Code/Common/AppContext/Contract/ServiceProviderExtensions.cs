using System;

namespace ChessMath.Shared.Common
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider provider) where T : class
        {
            var type = typeof(T);

            return provider.GetService(type) as T 
                   ?? throw new InvalidOperationException($"Cannot find Service of type: {type}");
        }

        public static T TryGetService<T>(this IServiceProvider provider) where T : class =>
            provider.GetService(typeof(T)) as T;
    }
}
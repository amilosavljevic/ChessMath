using System;
using ChessMath.Shared.Common.AppContextNs;

namespace ChessMath.Shared.Common
{
    public interface IContext : IServiceProvider, IEventBus, IDisposable
    {
        public ContextState State { get; }

        // This two methods should have default implementation inside this interface but that C# feature is not available
        // At first I implemented it as extension methods but you will often use it during initialization so...
        T GetService<T>() where T : class;
        T TryGetService<T>() where T : class;
    }

    public static class ContextExtensions
    {
        public static T GetSettings<T>(this IContext context) where T : class =>
            context.GetService<ISettingsProvider>().GetSettings<T>();

        public static bool IsInitialized(this IContext context) =>
            context.State == ContextState.Initialized;

        public static LocalizedString Localize(this IContext context, string key) =>
            context.GetService<ILocalizationProvider>().Localize(key);
    }
}

using System;

namespace ChessMath.Shared.Common
{
    public static class GlobalContext
    {
        private static IContext instance;

        public static IContext TryGet() => instance;

        public static IContext Get() =>
            instance
            ?? throw new NullReferenceException($"Global app context is not initialized. Call `{nameof(GlobalContext)}.{nameof(Set)}()` first.");

        public static void Set(IContext context)
        {
            instance?.Dispose();
            instance = context;
        }

        // Shortcuts
        public static T GetService<T>() where T : class => Get().GetService<T>();
        public static T TryGetService<T>() where T : class => Get().TryGetService<T>();
        public static T GetSettings<T>() where T : class => Get().GetSettings<T>();

        public static LocalizedString Localize(string key) =>
            Get().Localize(key);

        public static bool IsReleaseBuild =>
            !IsDevelopmentBuild;

        public static bool IsDevelopmentBuild =>
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            true;
#else
            false;
#endif

        public static bool IsInUnityEditor =>
#if UNITY_EDITOR
            true;
#else
            false;
#endif
    }
}

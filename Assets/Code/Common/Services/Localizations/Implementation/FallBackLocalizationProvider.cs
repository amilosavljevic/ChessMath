namespace ChessMath.Shared.Common.Localizations
{
    public class FallBackLocalizationProvider : ILocalizationProvider
    {
        private readonly ILocalizationProvider main;
        private readonly ILocalizationProvider fallback;

        public FallBackLocalizationProvider(ILocalizationProvider main, ILocalizationProvider fallback)
        {
            this.main = main;
            this.fallback = fallback;
        }

        public LocalizedString Localize(string key)
        {
            var result = main.Localize(key);
            return result.IsDefault ? fallback.Localize(key) : result;
        }
    }

    public static class FallBackLocalizationProviderExtensions
    {
        public static FallBackLocalizationProvider FallbackTo(this ILocalizationProvider main, ILocalizationProvider fallback) =>
            new FallBackLocalizationProvider(main, fallback);
    }
}

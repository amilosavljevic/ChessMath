namespace ChessMath.Shared.Common.Localizations
{
    public class ReturnKeyLocalizationProvider : ILocalizationProvider
    {
        public LocalizedString Localize(string key) => new(key, key);
    }
}

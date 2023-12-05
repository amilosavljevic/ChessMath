namespace ChessMath.Shared.Common.Localizations
{
    public class LocalizationProvider : ILocalizationProvider
    {
        private readonly ILanguageDictionary language;

        public LocalizationProvider(ILanguageDictionary language) =>
            this.language = language;

        public LocalizedString Localize(string key)
        {
            var value = language.Get(key);
            return value == null ? default : new LocalizedString(key, value);
        }
    }
}

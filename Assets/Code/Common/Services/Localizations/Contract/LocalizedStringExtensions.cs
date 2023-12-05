namespace ChessMath.Shared.Common
{
    public static class LocalizedStringExtensions
    {
        public static LocalizedString WithReplacedPlaceholders(this LocalizedString localizedString, params object[] replacements)
        {
            var replaced = string.Format(localizedString.Value, replacements);
            return new LocalizedString(localizedString.Key, replaced);
        }
    }
}

namespace ChessMath.Shared.Common
{
    public readonly struct LocalizedString
    {
        public readonly string Key;
        public readonly string Value;

        public LocalizedString(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator string(LocalizedString localizedString) =>
            localizedString.Value;

        public bool IsDefault =>
            Key == null && Value == null;

        public LocalizedString WithValue(string newValue) =>
            new LocalizedString(Key, newValue);
    }
}

namespace ChessMath.Shared.Common
{
    public static class CharToStringCache
    {
        private static readonly Cache<char, string> cache =
            new Cache<char, string>(v => v.ToString());

        public static string ToStringCached(this char key) =>
            cache.Get(key);
    }
}
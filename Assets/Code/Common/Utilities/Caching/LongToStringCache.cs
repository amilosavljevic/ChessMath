namespace ChessMath.Shared.Common
{
	public static class LongToStringCache
	{
		private static readonly Cache<long, string> cache =
			new Cache<long, string>(v => v.ToString());

		public static string ToStringCached(this long key) =>
			cache.Get(key);
		
		public static string ToStringCached(this int key) =>
			cache.Get(key);
	}
}
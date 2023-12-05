namespace ChessMath.Shared.Common
{
	public static class OrderStringCache
	{
		private static readonly Cache<long, string> cache =
			new Cache<long, string>(v => v + ".");

		public static string ToOrderString(this long key) =>
			cache.Get(key);
		
		public static string ToOrderString(this int key) =>
			cache.Get(key);
	}
}
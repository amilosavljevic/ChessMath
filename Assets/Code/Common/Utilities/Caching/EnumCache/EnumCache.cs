using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common
{
	/// <summary>
	/// Cache for enum names and values (to reduce allocations).
	/// </summary>
	public static class EnumCache
	{
		private static readonly Dictionary<Type, ISingleEnumCache> values = new Dictionary<Type, ISingleEnumCache>();

		public static IReadOnlyList<T> GetValues<T>() where T : Enum =>
			Get<T>().Values;

		public static IReadOnlyList<string> GetNames<T>() where T : Enum =>
			Get<T>().Names;

		public static string ToStringCached<T>(this T enumValue) where T : Enum =>
			Get<T>().ValuesToNames[enumValue];

		private static SingleEnumCache<T> Get<T>()
			where T : Enum
		{
			var type = typeof(T);
			
			if (!values.TryGetValue(type, out var cache))
			{
				var newCache = new SingleEnumCache<T>();
				values.Add(type, newCache);
				cache = newCache;
			}

			return (SingleEnumCache<T>)cache;
		}
	}
}
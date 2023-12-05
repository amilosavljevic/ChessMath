using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessMath.Shared.Common
{
	internal class SingleEnumCache<T> : ISingleEnumCache
		where T:Enum
	{
		public readonly Type EnumType;
		public readonly IReadOnlyList<T> Values;
		public readonly IReadOnlyList<string> Names;
		public readonly IReadOnlyDictionary<T, string> ValuesToNames;

		public SingleEnumCache()
		{
			EnumType = typeof(T);
			Values = Enum.GetValues(EnumType)
				.Cast<T>()
				.OrderBy(t=>t)
				.ToList();
			
			Names = Enum.GetNames(EnumType)
				.OrderBy(n=>n)
				.ToArray();

			ValuesToNames = Values.ToDictionary(v => v, v => v.ToString());
		}
	}
}
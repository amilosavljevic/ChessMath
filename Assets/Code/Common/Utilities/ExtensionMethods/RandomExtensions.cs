using System;

namespace ChessMath.Shared.Common
{
	public static class RandomExtensions
	{
		public static bool NextBoolean(this Random random) =>
			random.NextDouble() >= 0.5;
	}
}
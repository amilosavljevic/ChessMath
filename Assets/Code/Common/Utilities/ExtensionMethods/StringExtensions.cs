using System;
using System.Runtime.CompilerServices;

namespace ChessMath.Shared.Common
{
    public static class StringExtensions
    {
        private static readonly string[] eolCombinations = new string[]{"\r\n", "\r", "\n"};

        public static string[] SplitLines(this string text) =>
            text.Split(eolCombinations, StringSplitOptions.None);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhiteSpace(this string value) =>
            string.IsNullOrWhiteSpace(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(this string value) =>
            string.IsNullOrEmpty(value);
    }
}

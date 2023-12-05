using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMath.Shared.Common
{
    public static class QuickJsonExtensionMethods
    {
        public static string ToJsonArrayString<T>(this IEnumerable<T> items, Action<StringBuilder, T> appender) =>
            items.JoinToString(appender, ",", "[", "]");
        
        public static string ToJsonArrayString<T>(this IEnumerable<T> items, Func<T, string> converter) =>
            items.JoinToString(converter, ",", "[", "]");
    }
}
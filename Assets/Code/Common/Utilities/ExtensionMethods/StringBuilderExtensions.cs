using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMath.Shared.Common
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendRange(this StringBuilder sb, IEnumerable<string> items)
        {
            foreach (var item in items)
                sb.Append(item);

            return sb;
        }
        
        public static StringBuilder AppendRange<T>(this StringBuilder sb, IEnumerable<T> items)
        {
            foreach (var item in items)
                sb.Append(item.ToString());
            
            return sb;
        }

        public static StringBuilder AppendLines(this StringBuilder sb, IEnumerable<string> items) =>
            sb.AppendRange(items, Environment.NewLine);

        public static StringBuilder AppendRange(this StringBuilder sb, IEnumerable<string> items, string separator)
        {
            if (string.IsNullOrEmpty(separator))
                return AppendRange(sb, items);

            using var enumerator = items.GetEnumerator();

            if (enumerator.MoveNext())
                sb.Append(enumerator.Current);

            while (enumerator.MoveNext())
            {
                sb.Append(separator);
                sb.Append(enumerator.Current);
            }
            
            return sb;
        }
        
        public static StringBuilder AppendRange<T>(this StringBuilder sb, IEnumerable<T> items, Action<StringBuilder, T> appender)
        {
            foreach (var item in items)
                appender(sb, item);

            return sb;
        }

        public static StringBuilder AppendRange<T>(this StringBuilder sb, IEnumerable<T> items, Action<StringBuilder, T> appender, string separator)
        {
            if (string.IsNullOrEmpty(separator))
            {
                sb.AppendRange(items, appender);
                return sb;
            }
            
            using var enumerator = items.GetEnumerator();

            if (enumerator.MoveNext())
                appender(sb, enumerator.Current);

            while (enumerator.MoveNext())
            {
                sb.Append(separator);
                appender(sb, enumerator.Current);
            }

            return sb;
        }

        public static string JoinToString<T>(this IEnumerable<T> items, Action<StringBuilder, T> appender, string separator = null, string prefix = null, string suffix = null)
        {
            var sb = new StringBuilder();
            
            if (prefix != null)
                sb.Append(prefix);

            sb.AppendRange(items, appender, separator);

            if (suffix != null)
                sb.Append(suffix);

            return sb.ToString();
        }

        public static string JoinToString<T>(this IEnumerable<T> items, Func<T, string> convertor, string separator = null, string prefix = null, string suffix = null) =>
            items.JoinToString((sb, item) => sb.Append(convertor(item)), separator, prefix, suffix);

        public static string JoinToString(this IEnumerable<string> items, string separator = null, string prefix = null, string suffix = null) =>
            items.JoinToString((sb, s) => sb.Append(s), separator, prefix, suffix);

        public static string JoinToString(this IEnumerable<char> items, string separator = null, string prefix = null, string suffix = null) =>
            items.JoinToString((sb, s) => sb.Append(s), separator, prefix, suffix);
    }
}

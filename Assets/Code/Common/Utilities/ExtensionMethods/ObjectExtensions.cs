using System;

namespace ChessMath.Shared.Common
{
    public static class ObjectExtensions
    {
        public static T With<T>(this T target, Action<T> modifier)
        {
            modifier(target);
            return target;
        }
    }
}
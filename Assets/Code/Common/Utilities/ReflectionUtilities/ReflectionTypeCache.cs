using System;

namespace ChessMath.Shared.Common
{
    public static class ReflectionTypeCache
    {
        private static readonly Cache<Type, CachedType> cache = new Cache<Type, CachedType>(t =>
            new CachedType(t));

        public static CachedType GetCachedType(this Type type) =>
            cache.Get(type);

        public static CachedType GetCachedType(this object obj) =>
            obj.GetType().GetCachedType();
    }

    public class CachedType
    {
        private readonly Type type;
        private string name;

        public CachedType(Type type) =>
            this.type = type;

        public string Name =>
            name ??= type.Name;
    }
}
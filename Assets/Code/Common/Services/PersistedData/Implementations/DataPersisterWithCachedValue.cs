using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.PersistedData
{
    public class DataPersisterWithCachedValue : DataPersisterDecorator
    {
        private readonly Dictionary<Type, object> cache = new Dictionary<Type, object>();

        public DataPersisterWithCachedValue(IDataPersister actualPersister)
            : base(actualPersister) { }

        public override T Get<T>()
        {
            var type = typeof(T);

            if (cache.TryGetValue(type, out var value))
                return (T)value;

            var newValue =  base.Get<T>();
            cache[type] = newValue;

            return newValue;
        }

        public override object Get(Type type)
        {
            if (cache.TryGetValue(type, out var value))
                return value;

            var newValue =  base.Get(type);
            cache[type] = newValue;

            return newValue;
        }

        public override void Update<T>(T newValue)
        {
            cache[typeof(T)] = newValue;
            base.Update(newValue);
        }

        public override void Update(Type type, object newValue)
        {
            cache[type] = newValue;
            base.Update(type, newValue);
        }

        public override void Delete<T>()
        {
            base.Delete<T>();
            InvalidateCachedValue<T>();
        }

        public override void Delete(Type type)
        {
            base.Delete(type);
            InvalidateCachedValue(type);
        }

        public void InvalidateCachedValue<T>() =>
            InvalidateCachedValue(typeof(T));

        public void InvalidateCachedValue(Type type) =>
            cache.Remove(type);

        public void InvalidateAllCachedValues() =>
            cache.Clear();
    }

    public static class DataPersisterWithCacheExtensions
    {
        public static DataPersisterWithCachedValue WithCaching(this IDataPersister actualPersister) =>
            new DataPersisterWithCachedValue(actualPersister);
    }
}

using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.PersistedData
{
    public class DataPersisterWithDefaultValues : DataPersisterDecorator
    {
        private readonly Dictionary<Type, IEntry> defaultValuesFactory =
            new Dictionary<Type, IEntry>();

        public DataPersisterWithDefaultValues(IDataPersister actualPersister)
            : base(actualPersister)
        {
        }

        public override T Get<T>()
        {
            var value = base.Get<T>();

            return !EqualityComparer<T>.Default.Equals(value, default)
                ? value
                : CreateDefault<T>();
        }

        public override object Get(Type type)
        {
            var value = base.Get(type);
            return value ?? CreateDefault(type);
        }

        public void AddDefault<T>(Func<T> factory) =>
            defaultValuesFactory.Add(typeof(T), new Entry<T>(factory));

        public T CreateDefault<T>() =>
            defaultValuesFactory.TryGetValue(typeof(T), out var entry)
                ? ((Entry<T>)entry).CreateDefaultValue()
                : Activator.CreateInstance<T>();

        public object CreateDefault(Type type) =>
            defaultValuesFactory.TryGetValue(type, out var entry)
                ? entry.CreateDefaultValueAndBox()
                : Activator.CreateInstance(type);

        private interface IEntry
        {
            public object CreateDefaultValueAndBox();
        }

        private class Entry<T> : IEntry
        {
            private readonly Func<T> factory;

            public Entry(Func<T> factory) =>
                this.factory = factory;

            public T CreateDefaultValue() =>
                factory();

            public object CreateDefaultValueAndBox() =>
                CreateDefaultValue();
        }
    }

    public static class DataPersisterWithDefaultValuesExtensions
    {
        public static DataPersisterWithDefaultValues WithDefaultsPreInstantiated(this IDataPersister originalPersister) =>
            GetPersisterWithDefaults(originalPersister);

        public static DataPersisterWithDefaultValues WithDefault<T>(this IDataPersister originalPersister, Func<T> factory)
        {
            var persisterWithDefaults = GetPersisterWithDefaults(originalPersister);
            persisterWithDefaults.AddDefault(factory);
            return persisterWithDefaults;
        }

        private static DataPersisterWithDefaultValues GetPersisterWithDefaults(IDataPersister originalPersister) =>
            originalPersister switch
            {
                DataPersisterWithDefaultValues dataPersisterWithDefaultValues => dataPersisterWithDefaultValues,
                _ => new DataPersisterWithDefaultValues(originalPersister)
            };
    }
}

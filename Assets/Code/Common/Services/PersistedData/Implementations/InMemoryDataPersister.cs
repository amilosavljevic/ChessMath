using System;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.PersistedData
{
    public class InMemoryDataPersister : IDataPersister
    {
        public class Holder
        {
            public object Value;
        }

        private readonly Dictionary<Type, Holder> data = new Dictionary<Type, Holder>();

        public bool CanPersist(Type type) => true;
        public bool CanPersist<T>() => true;

        public T Get<T>() =>
            (T)Get(typeof(T));

        public object Get(Type type) =>
            GetHolder(type).Value;

        public Holder GetHolder(Type type)
        {
            if (data.TryGetValue(type, out var holder))
                return holder;

            holder = new Holder();
            data[type] = holder;

            if (type.IsValueType)
                holder.Value = Activator.CreateInstance(type);

            return holder;
        }

        public void Update<T>(T newValue) =>
            GetHolder(typeof(T)).Value = newValue;

        public void Update(Type type, object newValue) =>
            GetHolder(type).Value = newValue;

        public void Delete<T>() =>
            data.Remove(typeof(T));

        public void Delete(Type type) =>
            data.Remove(type);
    }
}

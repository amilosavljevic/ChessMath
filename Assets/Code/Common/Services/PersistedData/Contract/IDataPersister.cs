using System;

namespace ChessMath.Shared.Common
{
    public interface IDataPersister
    {
        public bool CanPersist(Type type);
        public bool CanPersist<T>();

        public T Get<T>();
        public object Get(Type type);

        public void Update<T>(T newValue);
        public void Update(Type type, object newValue);

        public void Delete<T>();
        public void Delete(Type type);
    }

    public static class DataPersisterExtensions
    {
        public static Persisted<T> GetPersistedDataHandle<T>(this IDataPersister persister) =>
            new Persisted<T>(persister);
    }
}

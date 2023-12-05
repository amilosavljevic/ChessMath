using System;

namespace ChessMath.Shared.Common
{
    public readonly struct Persisted<T> : IPersisted
    {
        private readonly IDataPersister persister;

        public Persisted (IDataPersister persister) =>
            this.persister = persister;

        public T Get() =>
            persister.Get<T>();

        public void Update(T newValue) =>
            persister.Update(newValue);

        public void Delete() =>
            persister.Delete<T>();

        object IPersisted.GetBoxed() =>
            Get();

        void IPersisted.UnboxAndUpdate(object boxedValue) =>
            Update((T)boxedValue);
    }

    public static class PersistedExtensions
    {
        public static void Modify<T>(Persisted<T> persisted, Action<T> modifyFunction)
            where T : class
        {
            var value = persisted.Get();
            modifyFunction(value);
            persisted.Update(value);
        }

        public static void Modify<T>(Persisted<T> persisted, Func<T, T> modifyFunction)
            where T : struct
        {
            var value = persisted.Get();
            persisted.Update(modifyFunction(value));
        }
    }

    public interface IPersisted
    {
        public object GetBoxed();
        public void UnboxAndUpdate(object newValue);
        public void Delete();
    }
}

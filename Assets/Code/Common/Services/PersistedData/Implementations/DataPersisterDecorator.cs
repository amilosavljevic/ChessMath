using System;

namespace ChessMath.Shared.Common.PersistedData
{
    public abstract class DataPersisterDecorator : IDataPersister
    {
        private readonly IDataPersister actualPersister;

        protected DataPersisterDecorator(IDataPersister actualPersister) =>
            this.actualPersister = actualPersister;

        public virtual bool CanPersist(Type type) => actualPersister.CanPersist(type);
        public virtual bool CanPersist<T>() => actualPersister.CanPersist<T>();
        public virtual T Get<T>() => actualPersister.Get<T>();
        public virtual object Get(Type type) => actualPersister.Get(type);
        public virtual void Update<T>(T newValue) => actualPersister.Update(newValue);
        public virtual void Update(Type type, object newValue) => actualPersister.Update(type, newValue);
        public virtual void Delete<T>() => actualPersister.Delete<T>();
        public virtual void Delete(Type type) => actualPersister.Delete(type);
    }
}

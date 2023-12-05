using System;
using System.IO;
using ChessMath.Shared.Common.Json;

namespace ChessMath.Shared.Common.PersistedData
{
    public class JsonFileDataPersister : IDataPersister
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly Func<Type, string> filePathResolver;
        private readonly Cache<string, string> directoryPathCache = new Cache<string, string>(Path.GetDirectoryName);

        public JsonFileDataPersister(IJsonSerializer jsonSerializer, Func<Type, string> filePathResolver)
        {
            this.jsonSerializer = jsonSerializer;
            this.filePathResolver = filePathResolver;
        }

        public bool CanPersist(Type type) => true;

        public bool CanPersist<T>() => true;

        public T Get<T>()
        {
            var path = GetPath(typeof(T));

            return File.Exists(path)
                ? jsonSerializer.Deserialize<T>(File.ReadAllText(path))
                : default;
        }

        public object Get(Type type)
        {
            var path = GetPath(type);

            return File.Exists(path)
                ? jsonSerializer.Deserialize(type, File.ReadAllText(path))
                : default;
        }

        private string GetPath(Type type) =>
            filePathResolver.Invoke(type);

        public void Update<T>(T newValue)
        {
            var path = GetPath(typeof(T));
            var json = jsonSerializer.Serialize(newValue);
            MakeSureDirectoryExists(path);
            File.WriteAllText(path, json);
        }

        public void Update(Type type, object newValue)
        {
            var path = GetPath(newValue.GetType());
            var json = jsonSerializer.Serialize(newValue);
            MakeSureDirectoryExists(path);
            File.WriteAllText(path, json);
        }

        private void MakeSureDirectoryExists(string filePath)
        {
            var directoryPath = directoryPathCache.Get(filePath);
            Directory.CreateDirectory(directoryPath);
        }

        public void Delete<T>()
        {
            var path = GetPath(typeof(T));

            if (File.Exists(path))
                File.Delete(path);
        }

        public void Delete(Type type)
        {
            var path = GetPath(type);

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}

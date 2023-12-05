using System;
using System.Collections.Generic;
using System.Reflection;

namespace ChessMath.Shared.Common.SettingsProviderNs
{
    /// <summary>
    /// Helper class to caches values of all fields and properties and pack them into dictionary that maps
    /// SettingsType => SettingsInstance.
    /// </summary>
    public class SettingsCache
    {
        private readonly object schema;
        private readonly Dictionary<Type, object> nestedSettings = new Dictionary<Type, object>();

        public SettingsCache(object schema)
        {
            this.schema = schema
                          ?? throw new ArgumentNullException(nameof(schema));

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var schemaType = schema.GetType();

            foreach (var fieldInfo in schemaType.GetFields(bindingFlags))
            {
                var nestedSetting =
                    fieldInfo.GetValue(schema)
                    ?? throw new NullReferenceException($"Nested settings is null for {schemaType.Name}.{fieldInfo.Name}.");

                AddNestedSetting(nestedSetting);
            }

            foreach (var propertyInfo in schema.GetType().GetProperties(bindingFlags))
            {
                var nestedSetting =
                    propertyInfo.GetValue(schema)
                    ?? throw new NullReferenceException($"Nested settings is null for {schemaType.Name}.{propertyInfo.Name}.");

                AddNestedSetting(nestedSetting);
            }
        }

        private void AddNestedSetting(object settings)
        {
            var type = settings.GetType();
            nestedSettings.Add(type, settings);

            var interfaces = type.GetInterfaces();
            foreach (var interfaceType in interfaces)
                nestedSettings.Add(interfaceType, settings);
        }

        public T Get<T>() =>
            nestedSettings.TryGetValue(typeof(T), out var settings)
                ? (T)settings
                : throw new ArgumentOutOfRangeException(nameof(T), $"Cannot find fields of type{typeof(T)} in {schema.GetType().Name}");
    }
}

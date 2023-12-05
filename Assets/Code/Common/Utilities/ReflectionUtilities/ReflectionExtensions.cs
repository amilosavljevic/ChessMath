using System;
using System.Reflection;

namespace ChessMath.Shared.Common
{
	public static class ReflectionExtensions
	{
		/// <summary>
		/// Set property value using reflection.
		/// </summary>
		public static void SetPropertyValue(this object target, string propertyName, object value) =>
            GetPropertyInfo(target, propertyName)
                .SetValue(target, value);

        /// <summary>
        /// Get property value using reflection.
        /// </summary>
        public static object GetPropertyValue(this object target, string propertyName) =>
            GetPropertyInfo(target, propertyName)
                .GetValue(target);

        public const BindingFlags StaticFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public static object GetStaticPropertyValue(this Type type, string propertyName)
        {
            var propertyInfo = type.GetProperty(propertyName, StaticFlags)
                ?? throw new InvalidOperationException($"Cannot find property `{propertyName}` on a object of type: {type}");
            return propertyInfo.GetValue(null);
        }

        private static PropertyInfo GetPropertyInfo(object target, string propertyName)
        {
            var type = target?.GetType()
                       ?? throw new ArgumentNullException(nameof(target));
            var propertyInfo =
                type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?? throw new InvalidOperationException($"Cannot find property `{propertyName}` on a object of type: {type}");
            return propertyInfo;
        }

        /// <summary>
		/// Set field value using reflection.
		/// </summary>
		public static void SetFieldValue(this object target, string propertyName, object value)
        {
            var fieldInfo = GetFieldInfo(target, propertyName);
            fieldInfo.SetValue(target, value);
        }

        /// <summary>
        /// Get field value using reflection.
        /// </summary>
        public static object GetFieldValue(this object target, string propertyName)
        {
            var fieldInfo = GetFieldInfo(target, propertyName);
            return fieldInfo.GetValue(target);
        }

        private static FieldInfo GetFieldInfo(object target, string propertyName)
        {
            var type = target?.GetType()
                       ?? throw new ArgumentNullException(nameof(target));
            var fieldInfo = type.GetField(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            ?? throw new InvalidOperationException(
                                $"Cannot find property `{propertyName}` on a object of type: {type}");
            return fieldInfo;
        }

        public static T InvokeStaticMethod<T>(this Type type, string methodName, params object[] parameteres) =>
            (T)InvokeStaticMethod(type, methodName, parameteres);

        public static object InvokeStaticMethod(this Type type, string methodName, params object[] parameteres)
        {
            var methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                ?? throw new InvalidOperationException($"Cannot find method: {type.Name}.{methodName}");

            parameteres ??= Array.Empty<object>();
            return methodInfo.Invoke(null, parameteres);
        }

        /// <summary>
        /// Set property value using reflection.
        /// </summary>
        public static object InvokeMethod(this object target, string methodName, params object[] parameters)
        {
            var type = target.GetType();
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                ?? throw new InvalidOperationException($"Failed to find method {methodName} in type: {type}.");

            // TODO: make sure we got correct overload by checking parameters type...
            return method.Invoke(target, parameters);
        }
    }
}

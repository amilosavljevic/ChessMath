/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ChessMath.Shared.Common.SettingsProviderNs
{
	/// <summary>
	/// Utility class to help with JSON deserialization and applying overrides to schema.
	/// </summary>
	public static class SettingsUtilities
	{
		private static  readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
		{
			ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ObjectCreationHandling = ObjectCreationHandling.Reuse,
			MissingMemberHandling = MissingMemberHandling.Ignore,
            Converters = {new StringEnumConverter()},
			ContractResolver = new PrivateResolver(),
		};

		public static void ApplyOverrides(object obj, string jsonContent)
        {
            JsonConvert.PopulateObject(jsonContent, obj, jsonSerializerSettings);
        }

		public class PrivateResolver : DefaultContractResolver
		{
			protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
			{
				var allProperties = type
					.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.Cast<MemberInfo>();

				var allFields =
					type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

				return allProperties
					.Concat(allFields)
					.Select(p => CreateProperty(p, memberSerialization))
					.ToList();
			}

			protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
			{
				var prop = base.CreateProperty(member, memberSerialization);

				if (!prop.Writable) {
					var property = member as PropertyInfo;
					var hasPrivateSetter = property?.GetSetMethod(true) != null;
					prop.Writable = hasPrivateSetter || member is FieldInfo;
				}

				return prop;
			}
		}
	}
}
*/

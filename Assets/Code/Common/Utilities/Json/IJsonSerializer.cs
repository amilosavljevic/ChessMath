using System;

namespace ChessMath.Shared.Common.Json
{
    public interface IJsonSerializer
    {
        string Serialize (object objectToSerialize);
        object Deserialize (Type objectType, string json);

        string Serialize<T>(T objToSerialize);
        T Deserialize<T>(string json);

        object DeserializeInto(string json, object existingObject);
    }
}

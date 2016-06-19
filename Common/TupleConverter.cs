using System;
using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// This converts the correct json to a Tuple with 3 items
    /// </summary>
    /// <typeparam name="T1">First item in tuple</typeparam>
    /// <typeparam name="T2">Second item in tuple</typeparam>
    /// <typeparam name="T3">Third item in tuple</typeparam>
    /// <remarks>Since the DiffService Api is using DefaultContractResolver instead of
    /// JsonMediaTypeFormatter the serialized tuple from the service contains keys like:
    /// {   "Item1": 1,   "Item2": "a" } instead of {   "m_Item1": 1,   "m_Item2": "a" }</remarks>
    public class TupleConverter<T1, T2, T3> : JsonConverter
    {
        #region Overrides of JsonConverter

        public override bool CanConvert(Type objectType)
        {
            return typeof(Tuple<T1, T2, T3>) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = Newtonsoft.Json.Linq.JObject.Load(reader);

            var target = new Tuple<T1, T2, T3>(jObject["Item1"].ToObject<T1>(),
                                               jObject["Item2"].ToObject<T2>(),
                                               jObject["Item3"].ToObject<T3>());

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        } 

        #endregion
    }
}

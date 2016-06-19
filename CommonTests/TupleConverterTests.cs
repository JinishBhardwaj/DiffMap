using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Common.Tests
{
    [TestClass()]
    public class TupleConverterTests
    {
        /// <summary>
        /// Test if the Tuple is properly Serialized and Deserialized
        /// to and from Json using the TupleConverter
        /// </summary>
        [TestMethod()]
        public void Can_Deserialize_Using_Converter_Test()
        {
            var tuple = Tuple.Create<int, string, string>(1, "a", "b");
            var json = JsonConvert.SerializeObject(tuple);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new TupleConverter<int, string, string>());

            var deserializedObject = JsonConvert.DeserializeObject<Tuple<int, string, string>>(json, settings);
            Assert.AreEqual(tuple.Item1, deserializedObject.Item1);
            Assert.AreEqual(tuple.Item2, deserializedObject.Item2);
            Assert.AreEqual(tuple.Item3, deserializedObject.Item3);
        }
    }
}
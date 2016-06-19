using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiffService.Helpers.Tests
{
    [TestClass()]
    public class ThreadSafeDictionaryTests
    {
        private ThreadSafeDictionary<int, string> _dictionary;

        [TestInitialize]
        public void Initialize()
        {
            _dictionary = new ThreadSafeDictionary<int, string>();
        }

        [TestMethod()]
        public void Adds_Value_To_Dictionary()
        {
            Assert.AreEqual(0, _dictionary.Count());
            _dictionary.AddOrUpdate(1, "Test Value");
            Assert.AreEqual(1, _dictionary.Count());
        }

        [TestMethod]
        public void Updates_Value_If_Same_Key_Provided()
        {
            _dictionary.AddOrUpdate(1, "Second Test Value");
            Assert.AreEqual(_dictionary[1], "Second Test Value");
        }
    }
}
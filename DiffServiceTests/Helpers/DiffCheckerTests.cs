using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiffService.Helpers.Tests
{
    [TestClass()]
    public class DiffCheckerTests
    {
        [TestMethod()]
        public void Are_Equal_Size_Returns_True_If_Arrays_Are_Equal_Length()
        {
            var array1 = new byte[] { 1, 2, 3, 5, 6 };
            var array2 = new byte[] { 2, 4, 5, 6, 7 };
            var areEqual = array1.Length == array2.Length;
            Assert.AreEqual(DiffChecker.AreEqualSize(array1, array2), areEqual);
        }

        [TestMethod]
        public void Returns_Diff_When_Two_Arrays_Passed()
        {
            var array1 = new byte[] { 1, 2, 3, 4 };
            var array2 = new byte[] { 1, 4, 5, 6 };

            var diff = DiffChecker.GetDiff(array1, array2);
            Assert.AreEqual(typeof(List<Tuple<int, string, string>>), diff.Results.GetType());
            Assert.AreEqual(diff.Results.Count, 3);
        }

        [TestMethod]
        public void Returns_Diff_When_Two_Base64_String_Are_Passed()
        {
            var string1 = "Simple String 1";
            var encodedString1 = Convert.ToBase64String(Encoding.ASCII.GetBytes(string1));

            var string2 = "Simple String 2";
            var encodedString2 = Convert.ToBase64String(Encoding.ASCII.GetBytes(string2));

            var diff = DiffChecker.GetDiff(encodedString1, encodedString2);
            Assert.AreEqual(typeof(List<Tuple<int, string, string>>), diff.Results.GetType());
            Assert.AreEqual(diff.Results.Count, 1);
        }
    }
}
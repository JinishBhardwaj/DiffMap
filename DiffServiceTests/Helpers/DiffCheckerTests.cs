using System;
using System.Collections.Generic;
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

            var result = DiffChecker.GetDiff(array1, array2);
            Assert.AreEqual(typeof(List<Tuple<int, string, string>>), result.GetType());
            Assert.AreEqual(result.Count, 3);
        }
    }
}
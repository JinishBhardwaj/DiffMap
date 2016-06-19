using System;
using System.Collections.Generic;
using System.Text;

namespace DiffService.Helpers
{
    /// <summary>
    /// This class is responsible for calculating the diff of the 
    /// two byte arrays provided and return it as a Tuple[int, string, string]
    /// </summary>
    public static class DiffChecker
    {
        #region Methods

        /// <summary>
        /// Creates a Tuple with the diff results of the two provided byte
        /// arrays. The tuple contains (Index, Diff element from first array, Diff element from second array)
        /// </summary>
        /// <param name="left">First array</param>
        /// <param name="right">Second array</param>
        /// <returns>Diff result as a Tuple</returns>
        public static List<Tuple<int, string, string>> GetDiff(byte[] left, byte[] right)
        {
            var diffList = new List<Tuple<int, string, string>>();

            // Get the common length (Take the minimum to avoid any index out of range problems)
            // This will not happnen in our case since we step in only if arrays are same size
            int standardLength = Math.Min(left.Length, right.Length);
            for (int i = 0; i < standardLength; i++)
            {
                // Get the absolute value of the difference between the element
                // of the two arrays at the i'th index and if the value is greater 
                // than 0 then the byte notation is not the same and hence the two
                // elements are different
                var result = Math.Abs(left[i] - right[i]);
                if (result > 0)
                {
                    diffList.Add(Tuple.Create<int, string, string>(i,
                                                                   Encoding.ASCII.GetString(new byte[] { left[i] }),
                                                                   Encoding.ASCII.GetString(new byte[] { right[i] })));
                }
            }
            return diffList;
        }

        /// <summary>
        /// Returns if the two provided arrays are the same size
        /// </summary>
        /// <param name="left">First array</param>
        /// <param name="right">Second array</param>
        /// <returns></returns>
        public static bool AreEqualSize(byte[] left, byte[] right)
        {
            return (left.Length == right.Length);
        }

        #endregion

        #region Getting diff using Linq - Unused code

        //var diff = _streams["Left"].Select((x, y) => new { x, y })
        //            .Join
        //            (
        //                _streams["Right"].Select((x, y) => new { x, y }),
        //                x => x.y,
        //                x => x.y,
        //                (d1, d2) => Math.Abs(d1.x - d2.x)
        //            )
        //            .ToArray();

        //var diff = _streams["Left"].Zip(_streams["Right"], (d1, d2) => Math.Abs(d1 - d2)).ToArray();

        #endregion
    }
}
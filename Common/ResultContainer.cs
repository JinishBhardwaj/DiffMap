using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// This class is the response container returned by the endpoint
    /// with the results of the diff match operation
    /// </summary>
    public class ResultContainer
    {
        #region Constructors

        public ResultContainer()
        {
            Results = new List<Tuple<int, string, string>>();
        }

        #endregion

        #region Properties

        public Status Status { get; set; }
        public List<Tuple<int, string, string>> Results { get; set; } 

        #endregion
    }

    public enum Status
    {
        AreEqual,
        NotSameSize,
        SameSizeNotEqual
    }
}

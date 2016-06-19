using System.Collections.Generic;

namespace DiffService.Helpers
{
    /// <summary>
    /// This is a Thread Safe implementation of the Dictionary using 
    /// a simple locking mechanism. This is still better inperformance
    /// than using a Concurrent dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of Key</typeparam>
    /// <typeparam name="TValue">Type of Value</typeparam>
    public class ThreadSafeDictionary<TKey, TValue>: Dictionary<TKey, TValue>
    {
        #region Fields

        private readonly object _lockObject = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeDictionary{TKey, TValue}"/> class
        /// </summary>
        public ThreadSafeDictionary() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeDictionary{TKey, TValue}"/> class
        /// </summary>
        /// <param name="capacity">Number of elements the dictionary can hold</param>
        public ThreadSafeDictionary(int capacity) : base(capacity) { }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the provided value to the dictionary if 
        /// the key is not present in the dictionary, else
        /// updates the value against the key provided
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void AddOrUpdate(TKey key, TValue value)
        {
            lock (_lockObject)
            {
                if (this.ContainsKey(key))
                    this[key] = value;
                else
                    this.Add(key, value);
            }
        }

        #endregion
    }
}
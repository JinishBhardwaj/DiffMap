using System;

namespace Common
{
    /// <summary>
    /// This class contains extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a provided base64 string to byte[]
        /// </summary>
        /// <param name="encodedString">Base64 encoded string</param>
        /// <returns>Byte[]</returns>
        public static byte[] ToBytes(this string encodedString)
        {
            return Convert.FromBase64String(encodedString);
        }
    }
}
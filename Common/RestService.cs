using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Common
{
    /// <summary>
    /// Service interface to make Http calls to the Diff Service api endpoints
    /// </summary>
    public class RestService
    {
        /// <summary>
        /// Posts content as base 64 encoded string  to the endpoint
        /// </summary>
        /// <typeparam name="T">Content of type T</typeparam>
        /// <param name="uri">Endpoint Url</param>
        /// <param name="content">Content to post</param>
        /// <returns>Endpoint response</returns>
        public async Task<string> PostBase64Async<T>(string uri, T content) where T: class
        {
            using (var client = GetWrappedClient())
            {
                MemoryStream bsonMemoryStream = new MemoryStream();
                using (BsonWriter writer = new BsonWriter(bsonMemoryStream))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, JsonConvert.DeserializeObject(content.ToString()));
                }

                var bsonDataString = Convert.ToBase64String(bsonMemoryStream.ToArray());
                var response = await client.PostAsJsonAsync(uri, bsonDataString);
                return response.StatusCode.ToString();
            }
        }

        /// <summary>
        /// Posts content as bbyte[]  to the endpoint
        /// </summary>
        /// <typeparam name="T">Content of type T</typeparam>
        /// <param name="uri">Endpoint Url</param>
        /// <param name="content">Content to post</param>
        /// <returns>Endpoint response</returns>
        public async Task<string> PostAsByteArrayAsync<T>(string uri, T content) where T : class
        {
            using (var client = GetWrappedClient())
            {
                MemoryStream bsonMemoryStream = new MemoryStream();
                using (BsonWriter writer = new BsonWriter(bsonMemoryStream))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, JsonConvert.DeserializeObject(content.ToString()));
                }

                var bsonDataStream = new ByteArrayContent(bsonMemoryStream.ToArray());
                var response = await client.PostAsync(uri, bsonDataStream);
                return response.StatusCode.ToString();
            }
        }

        /// <summary>
        /// Fetches the result from the endpoint
        /// </summary>
        /// <typeparam name="T">Type of result to return</typeparam>
        /// <param name="uri">Endpoint Url</param>
        /// <returns>T</returns>
        public async Task<T> GetAsync<T>(string uri)
        {
            using (var client = GetWrappedClient())
            {
                var response = await client.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = DeserializeFromString<T>(content);
                    return data;
                }
                return default(T);
            }
        }

        #region Helpers

        /// <summary>
        /// Gets the HttpClient and sets the Accept headers to accept json
        /// </summary>
        /// <returns>HttpClient</returns>
        private HttpClient GetWrappedClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        /// <summary>
        /// Deserializes a string response from the endpoint 
        /// to the provided type using the TupleConverter
        /// </summary>
        /// <typeparam name="T">Type to deserialize the string to</typeparam>
        /// <param name="content">String to deserialize</param>
        /// <returns>Deserialized object of type T/></returns>
        private T DeserializeFromString<T>(string content)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new TupleConverter<int, string, string>());
            var data = JsonConvert.DeserializeObject<T>(content, settings);
            return (T)data;
        }

        #endregion
    }
}

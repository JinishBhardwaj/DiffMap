using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using static System.Console;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTaskAsync().Wait();
            ReadLine();
        }

        static async Task RunTaskAsync()
        {
            using (var client = GetWrappedClient())
            {
                string simpleJson = @"{
                                    'name': 'Jinish',
                                    'age': 35,
                                    'certifications': ['MCP', 'CSM'],
                                    'healthy': true,
                                    'dob': '1980-02-08T00:00:00',
                                    'issues': null,
                                    'car': {
                                                'year': 2011,
                                                'model': 'Toyota Innova SUV'
                                            }
                                   }";
                var simpleObject = JsonConvert.DeserializeObject(simpleJson);
                JsonSerializer serializer = new JsonSerializer();
                MemoryStream stream = new MemoryStream();
                using (BsonWriter writer = new BsonWriter(stream))
                {
                    serializer.Serialize(writer, simpleObject);
                }


                string simpleJson1 = @"{
                                    'name': 'Jeroen',
                                    'age': 36,
                                    'certifications': ['MCP', 'CSM'],
                                    'healthy': true,
                                    'dob': '1981-02-08T00:00:00',
                                    'issues': null,
                                    'car': {
                                                'year': 2012,
                                                'model': 'Toyota Innova SUV'
                                            }
                                   }";
                var simpleObject1 = JsonConvert.DeserializeObject(simpleJson1);
                JsonSerializer serializer1 = new JsonSerializer();
                MemoryStream stream1 = new MemoryStream();
                using (BsonWriter writer = new BsonWriter(stream1))
                {
                    serializer.Serialize(writer, simpleObject1);
                }

                var leftContent = new ByteArrayContent(stream.ToArray());
                var responseLeft = await client.PostAsync("http://localhost:61244//v1/diff/Left", leftContent);

                var rightContent = new ByteArrayContent(stream1.ToArray());
                var responseRight = await client.PostAsync("http://localhost:61244//v1/diff/Right", rightContent);

                WriteLine(string.Format("Left endpoint success: {0} \nRight endpoint success: {1}", responseLeft.StatusCode, responseRight.StatusCode));

                var resultResponse = await client.GetAsync("http://localhost:61244/v1/diff");
                if (resultResponse.StatusCode == HttpStatusCode.OK)
                {
                    var content = await resultResponse.Content.ReadAsStringAsync();
                    var data = DeserializeFromString<List<Tuple<int, string, string>>>(content);
                    foreach (var item in data)
                    {
                        WriteLine(string.Format("Index: {0} - Left: {1} - Right: {2}", item.Item1, item.Item2, item.Item3));
                    }
                }
            } 
        }

        #region HttpClient

        private static HttpClient GetWrappedClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            return client;
        }

        #endregion

        public static T DeserializeFromString<T>(string content)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new TupleConverter<int, string, string>());
            var data = JsonConvert.DeserializeObject<T>(content, settings);
            return (T)data;
        }
    }
}

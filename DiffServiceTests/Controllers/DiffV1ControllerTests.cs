using System.Net.Http;
using System.Text;
using System.Web.Http;
using DiffService.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;

namespace DiffService.Controllers.Tests
{
    [TestClass()]
    public class DiffV1ControllerTests
    {
        private HttpConfiguration _config;
        private ThreadSafeDictionary<string, byte[]> _dictionary;

        [TestInitialize]
        public void Initialize()
        {
            _config = new HttpConfiguration();
            WebApiConfig.Register(_config);
            _config.EnsureInitialized();

            _dictionary = new ThreadSafeDictionary<string, byte[]>
            {
                ["Left"] = new byte[] { 1, 2, 3, 4 },
                ["Right"] = new byte[] { 5, 6, 7, 8 }
            };
        }

        [TestMethod]
        public void Post_Left_Calls_Correct_Endpoint()
        {
            const string route = "/v1/diff/left";
            RouteAssert.HasApiRoute(_config, route, HttpMethod.Post);
            var content = Encoding.ASCII.GetBytes("Sample string");
            _config.ShouldMap(route).To<DiffV1Controller>(HttpMethod.Post, x => x.Left(content));
        }

        [TestMethod]
        public void Post_Right_Calls_Correct_Endpoint()
        {
            const string route = "/v1/diff/right";
            RouteAssert.HasApiRoute(_config, route, HttpMethod.Post);
            var content = Encoding.ASCII.GetBytes("Sample string");
            _config.ShouldMap(route).To<DiffV1Controller>(HttpMethod.Post, x => x.Right(content));
        }

        [TestMethod]
        public void Diff_Results_Calls_Correct_Endpoint()
        {
            const string route = "/v1/diff";
            RouteAssert.HasApiRoute(_config, route, HttpMethod.Get);
            _config.ShouldMap(route).To<DiffV1Controller>(HttpMethod.Get, x => x.Get());
        }
    }
}
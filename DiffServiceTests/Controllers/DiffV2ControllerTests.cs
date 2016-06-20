using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiffService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MvcRouteTester;
using System.Net.Http;

namespace DiffService.Controllers.Tests
{
    [TestClass()]
    public class DiffV2ControllerTests
    {
        private HttpConfiguration _config;

        [TestInitialize]
        public void Initialize()
        {
            _config = new HttpConfiguration();
            WebApiConfig.Register(_config);
            _config.EnsureInitialized();
        }

        [TestMethod]
        public void Post_Left_Calls_Correct_Endpoint()
        {
            const string route = "/v2/diff/left";
            RouteAssert.HasApiRoute(_config, route, HttpMethod.Post);
            var content = Encoding.ASCII.GetBytes("Sample string");
            _config.ShouldMap(route).To<DiffV2Controller>(HttpMethod.Post, x => x.Left(Convert.ToBase64String(content)));
        }

        [TestMethod]
        public void Post_Right_Calls_Correct_Endpoint()
        {
            const string route = "/v2/diff/right";
            RouteAssert.HasApiRoute(_config, route, HttpMethod.Post);
            var content = Encoding.ASCII.GetBytes("Sample string");
            _config.ShouldMap(route).To<DiffV2Controller>(HttpMethod.Post, x => x.Right(Convert.ToBase64String(content)));
        }

        [TestMethod]
        public void Diff_Results_Calls_Correct_Endpoint()
        {
            const string route = "/v2/diff";
            RouteAssert.HasApiRoute(_config, route, HttpMethod.Get);
            _config.ShouldMap(route).To<DiffV2Controller>(HttpMethod.Get, x => x.Get());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DiffService.Controllers
{
    [RoutePrefix("v2/diff")]
    public class DiffV2Controller : ApiController
    {
        [Route("left")]
        public IHttpActionResult Left()
        {
            return Ok();
        }

        [Route("right")]
        public IHttpActionResult Right()
        {
            return Ok();
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}

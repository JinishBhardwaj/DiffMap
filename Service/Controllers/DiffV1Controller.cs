using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DiffService.Helpers;
using DiffService.Helpers.UI;

namespace DiffService.Controllers
{
    /// <summary>
    /// This controller defines api endpoints to recieve and process the Json
    /// binary data from the client.
    /// </summary>
    [RoutePrefix("v1/diff")]
    public class DiffV1Controller : ApiController
    {
        #region Fields

        const string LeftKey = "Left";
        const string RightKey = "Right";
        private readonly object _syncRoot = new object();
        private static ThreadSafeDictionary<string, byte[]> _streams = new ThreadSafeDictionary<string, byte[]>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initialized a new instance of the <see cref="DiffV1Controller"/> class
        /// </summary>
        public DiffV1Controller()
        {
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// This endpoint recieves a Json binary data
        /// from the client
        /// </summary>
        /// <param name="content">Bson stream</param>
        /// <returns>Http result indicating if the request succeeded or failed</returns>
        [Route("left")]
        public IHttpActionResult Left([ModelBinder(typeof(ByteArrayModelBinder))]byte[] content)
        {
            try
            {
                _streams.AddOrUpdate(LeftKey, content);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This endpoint recieves a Json binary data
        /// from the client
        /// </summary>
        /// <param name="content">Bson stream</param>
        /// <returns>Http result indicating if the request succeeded or failed</returns>
        [Route("right")]
        public IHttpActionResult Right([ModelBinder(typeof(ByteArrayModelBinder))]byte[] content)
        {
            try
            {
                _streams.AddOrUpdate(RightKey, content);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// The diff result will be available at this endpoint
        /// </summary>
        /// <returns>Http result with the diff results</returns>
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var diffMap = new List<Tuple<int, string, string>>();
                if (DiffChecker.AreEqualSize(_streams[LeftKey], _streams[RightKey]))
                {
                    diffMap = DiffChecker.GetDiff(_streams[LeftKey], _streams[RightKey]);
                }
                _streams.Clear();
                return Ok(diffMap);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        #endregion
    }
}

using System.Web.Http;
using DiffService.Helpers;

namespace DiffService.Controllers
{
    /// <summary>
    /// This controller defines api endpoints to recieve and process the Json
    /// base64 encoded data from the client.
    /// </summary>
    [RoutePrefix("v2/diff")]
    public class DiffV2Controller : ApiController
    {
        #region Fields

        const string LeftKey = "Left";
        const string RightKey = "Right";
        private readonly object _syncRoot = new object();
        private static ThreadSafeDictionary<string, string> _streams = new ThreadSafeDictionary<string, string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initialized a new instance of the <see cref="DiffV2Controller"/> class
        /// </summary>
        public DiffV2Controller()
        {
        }

        #endregion

        /// <summary>
        /// This endpoint recieves a base 64 encoded string 
        /// </summary>
        /// <param name="encodedData">Base64 encoded content</param>
        /// <returns>Http result indicating whether the request succeeded or failed</returns>
        [HttpPost]
        [Route("left")]
        public IHttpActionResult Left([FromBody]string encodedData)
        {
            try
            {
                _streams.AddOrUpdate(LeftKey, encodedData);
                return Ok(encodedData);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This endpoint recieves a base 64 encoded string 
        /// </summary>
        /// <param name="encodedData">Base64 encoded content</param>
        /// <returns>Http result indicating whether the request succeeded or failed</returns>
        [HttpPost]
        [Route("right")]
        public IHttpActionResult Right([FromBody]string encodedData)
        {
            try
            {
                _streams.AddOrUpdate(RightKey, encodedData);
                return Ok(encodedData);
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
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                if (_streams?.Count > 0)
                {
                    var diffMap = DiffChecker.GetDiff(_streams[LeftKey], _streams[RightKey]);
                    _streams.Clear();
                    return Ok(diffMap);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

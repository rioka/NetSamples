using System;
using System.Collections.Generic;
using System.Web.Http;

namespace OWinCustomAuthSample.Controllers
{
  [RoutePrefix("sample")]
  public class SampleController : ApiController
  {
    #region Apis

    [Route("")]
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new[] { "value1", "value2", DateTime.UtcNow.ToUniversalTime().ToString() };
    }

    #endregion
  }
}
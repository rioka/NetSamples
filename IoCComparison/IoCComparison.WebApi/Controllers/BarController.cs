using System.Web.Http;
using IoCComparison.Core;

namespace IoCComparison.WebApi.Controllers {

   [RoutePrefix("bar")]
   public class BarController : ApiController {
      
      private readonly ITaskRunner _taskRunner;

      public BarController(ITaskRunner taskRunner) {
         _taskRunner = taskRunner;
      }

      [Route("")]
      [HttpGet]
      //[ResponseType(bool)]
      public IHttpActionResult Get() {

         return Ok(new {success = _taskRunner.Run()});
      }
   }
}
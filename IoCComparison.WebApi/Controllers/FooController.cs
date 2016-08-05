using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using IoCComparison.Core;

namespace IoCComparison.WebApi.Controllers {
   
   [RoutePrefix("foo")]
   public class FooController : ApiController {

      private static readonly Random Rgn = new Random();
      private readonly Lazy<IService> _service;
      private readonly ISecondService _secondService;

      private IService Service { get { return _service.Value; } }

      public FooController(Lazy<IService> service, ISecondService secondService) {
         _service = service;
         _secondService = secondService;
      }

      [Route("")]
      [HttpGet]
      [ResponseType(typeof(IEnumerable<int>))]
      public IHttpActionResult Get() {

         var min = Rgn.Next(1, 10);
         var max = Rgn.Next(min + 1, 3 * min + 1);
         return Ok(Enumerable.Range(min, max).ToArray());
      }

      protected override void Dispose(bool disposing) {
         
         Trace.WriteLine("Disposing " + GetType().Name);
         base.Dispose(disposing);
      }
   }
}
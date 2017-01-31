using System;
using System.Web.Mvc;
using IoCComparison.Core;

namespace IoCComparison.Autofac.MixedWebformsMvcSample.Controllers {

   public class ValueController : Controller {

      private readonly IService _service;

      public ValueController(IService service) {

         _service = service;
      }

      public JsonResult Get() {

         return Json(new {
            Value = DateTime.UtcNow.Millisecond,
            Service = _service.ToString()
         }, JsonRequestBehavior.AllowGet);
      }
   }
}
using System.Web.Mvc;

namespace IoCComparison.Autofac.MixedWebformsMvcSample.Controllers {

   public class ValueController : Controller {

      public JsonResult Get() {

         return Json("123", JsonRequestBehavior.AllowGet);
      }
   }
}
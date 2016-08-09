using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Web;

namespace IoCComparison.Autofac.WebformsSample {

   public partial class Global : HttpApplication, IContainerProviderAccessor {

      void Application_Start(object sender, EventArgs e) {
         
         // Code that runs on application startup
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);

         SetupContainer();
      }
   }
}
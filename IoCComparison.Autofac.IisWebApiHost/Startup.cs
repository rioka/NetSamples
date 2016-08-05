using System.Web.Http;
using IoCComparison.Autofac.IisWebApiHost;
using IoCComparison.Autofac.IisWebApiHost.IoC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace IoCComparison.Autofac.IisWebApiHost {
   
   public class Startup {

      public void Configuration(IAppBuilder appBuilder) {

         var config = new HttpConfiguration();
         config.MapHttpAttributeRoutes();

         config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );

         appBuilder.IoCSetup(config);
         appBuilder.UseWebApi(config);
      }
   }
}

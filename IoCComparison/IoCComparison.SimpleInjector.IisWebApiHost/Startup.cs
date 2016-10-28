using System.Web.Http;
using IoCComparison.SimpleInjector.IisWebApiHost;
using IoCComparison.SimpleInjector.IisWebApiHost.IoC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace IoCComparison.SimpleInjector.IisWebApiHost {
   
   public class Startup {

      public void Configuration(IAppBuilder appBuilder) {

         var config = new HttpConfiguration();
         config.MapHttpAttributeRoutes();

         config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );

         ContainerFactory.Build(config);
         appBuilder.UseWebApi(config);
      }
   }
}
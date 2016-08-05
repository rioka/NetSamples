using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace IoCComparison.Autofac.IisWebApiHost.IoC {

   public static class AppBuilderExtensions {

      public static void IoCSetup(this IAppBuilder appBuilder, HttpConfiguration config) {

         var builder = new ContainerBuilder();

         builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

         var container = builder.Build();
         config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

         // OWIN WEB API SETUP, IN THIS ORDER
         // http://autofac.readthedocs.io/en/latest/integration/webapi.html#owin-integration
         appBuilder.UseAutofacMiddleware(container);
         appBuilder.UseAutofacWebApi(config);
      }
   }
}
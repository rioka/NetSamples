using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using IoCComparison.WebApi.Controllers;

namespace IoCComparison.Autofac.IisWebApiHost.IoC.Installers {

   public class ControllersInstaller : global::Autofac.Module {
      
      protected override void Load(ContainerBuilder builder) {

         builder
            .RegisterApiControllers(typeof(FooController).Assembly)
            .ExternallyOwned();     // just to avoid having Dispose called twice for controllers
      }
   }
}
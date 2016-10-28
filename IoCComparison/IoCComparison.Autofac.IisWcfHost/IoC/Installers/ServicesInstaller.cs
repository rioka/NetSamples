using System.Linq;
using System.ServiceModel;
using Autofac;
using IoCComparison.WcfServices;

namespace IoCComparison.Autofac.IisWcfHost.IoC.Installers {
   
   public class ServicesInstaller : Module {

      protected override void Load(ContainerBuilder builder) {

         // register all types which has a ServiceContract attribute either by itself or in one of the implemented interfaces
         builder
            .RegisterTypes(
               typeof(IFooService).Assembly
                  .GetTypes()
                  .Where(t => t.GetCustomAttributes(typeof(ServiceContractAttribute), true).Any()
                              || t.GetInterfaces()
                                 .Any(i => i.IsPublic
                                      && i.GetCustomAttributes(typeof(ServiceContractAttribute), true).Any()))
                  .ToArray());
      }
   }
}
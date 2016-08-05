using Autofac;
using IoCComparison.Core;

namespace IoCComparison.Autofac.SpeedTest.IoC.Installers {
   
   public class NonDisposableTypeAsTransient : Module {
      
      protected override void Load(ContainerBuilder builder) {

         // Register non-disposable type as transient
         builder
            .RegisterType<SampleService>()
            .AsImplementedInterfaces()
            .PropertiesAutowired()
            .InstancePerDependency();
      }
   }
}
using Autofac;
using Autofac.Extras.DynamicProxy2;
using IoCComparison.Autofac.SpeedTest.Infrastructure;
using IoCComparison.Core;

namespace IoCComparison.Autofac.SpeedTest.IoC.Installers {
   
   public class NonDisposableTypeAsTransient : Module {
      
      protected override void Load(ContainerBuilder builder) {

         builder
            .RegisterType<DebugLogger>()
            .AsImplementedInterfaces()
            .SingleInstance();

         // Register non-disposable type as transient
         builder
            .RegisterType<SampleService>()
            .AsImplementedInterfaces()
            .PropertiesAutowired()                    // populate Logger property on SampleService
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DummyInterceptor).Name.ToLower())  // refers to the named service
            .InstancePerDependency();
      }
   }
}
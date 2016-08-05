using Autofac;
using Castle.DynamicProxy;
using IoCComparison.Autofac.SpeedTest.Infrastructure;

namespace IoCComparison.Autofac.SpeedTest.IoC.Installers {
   public class Interceptors : Module {
      protected override void Load(ContainerBuilder builder) {

         // register DummyInterceptor as a named service
         builder
            .Register(c => new DummyInterceptor())
            .Named<IInterceptor>(typeof(DummyInterceptor).Name.ToLower());
      }
   }
}

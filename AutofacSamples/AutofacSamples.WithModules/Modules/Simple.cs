using AutofacSamples.WithModules.Services;
using Autofac;

namespace AutofacSamples.WithModules.Modules {

   public class Simple : Base {

      protected override void Load(ContainerBuilder builder) {

         builder
            .RegisterType<Parser>()
            .AsImplementedInterfaces();
      }
   }
}

using AutofacSamples.WithModules.Services;
using Autofac;

namespace AutofacSamples.WithModules.Modules {

   public class Simple2 : Base {

      protected override void Load(ContainerBuilder builder) {

         builder
            .RegisterType<Repository>()
            .AsImplementedInterfaces();
      }
   }
}
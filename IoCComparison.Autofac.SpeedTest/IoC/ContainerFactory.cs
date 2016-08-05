using Autofac;
using Autofac.Core;

namespace IoCComparison.Autofac.SpeedTest.IoC {

   public static class ContainerFactory {

      public static IContainer Build(IModule module) {

         var builder = new ContainerBuilder();

         builder.RegisterModule(module);
         var container = builder.Build();

         return container;
      }
   }
}
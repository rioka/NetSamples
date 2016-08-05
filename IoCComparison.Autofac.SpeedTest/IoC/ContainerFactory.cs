using Autofac;
using Autofac.Core;
using Castle.Core.Internal;

namespace IoCComparison.Autofac.SpeedTest.IoC {

   public static class ContainerFactory {

      public static IContainer Build(params IModule[] modules) {

         var builder = new ContainerBuilder();

         foreach (var module in modules) {
            builder.RegisterModule(module);
         }
         var container = builder.Build();

         return container;
      }
   }
}
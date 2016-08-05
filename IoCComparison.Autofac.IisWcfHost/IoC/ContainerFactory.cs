using System.Reflection;
using Autofac;
using Autofac.Integration.Wcf;

namespace IoCComparison.Autofac.IisWcfHost.IoC {

   public static class ContainerFactory {

      public static IContainer Build() {

         var builder = new ContainerBuilder();

         builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

         var container = builder.Build();
         AutofacHostFactory.Container = container;

         return container;
      }
   }
}
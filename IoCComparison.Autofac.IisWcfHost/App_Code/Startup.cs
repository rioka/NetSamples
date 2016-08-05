using System;
using Autofac;
using IoCComparison.Autofac.IisWcfHost.IoC;

namespace IoCComparison.Autofac.IisWcfHost {
   
   public class Startup {

      private static IContainer _container;

      public static void AppInitialize() {

         _container = ContainerFactory.Build();
         AppDomain.CurrentDomain.DomainUnload += Shutdown;
      }

      private static void Shutdown(object sender, EventArgs e) {
         _container.Dispose();
      }
   }
}
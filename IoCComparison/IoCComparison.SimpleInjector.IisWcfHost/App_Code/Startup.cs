using System;
using IoCComparison.SimpleInjector.IisWcfHost.IoC;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.IisWcfHost {

   public class Startup {

      private static Container _container;

      public static void AppInitialize() {

         _container = ContainerFactory.Build();
         AppDomain.CurrentDomain.DomainUnload += Shutdown;
      }

      private static void Shutdown(object sender, EventArgs e) {
         _container.Dispose();
      }
   }
}
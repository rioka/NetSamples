using System;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.SpeedTest.IoC {

   public static class ContainerFactory {

      public static Container Build(Action<Container> registerTypes) {

         var container = new Container();

         registerTypes(container);

         container.Verify();
         return container;
      }
   }
}
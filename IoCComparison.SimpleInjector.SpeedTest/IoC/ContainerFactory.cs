using System;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.SpeedTest.IoC {

   public static class ContainerFactory {

      public static Container Build(params Action<Container>[] registerTypes) {

         var container = new Container();
         container.Options.PropertySelectionBehavior = new InjectPropertyBehavior(container);

         foreach (var action in registerTypes) {
            action(container);
         }

         container.Verify();
         return container;
      }
   }
}
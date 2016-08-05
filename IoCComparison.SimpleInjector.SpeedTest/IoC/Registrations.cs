using IoCComparison.Core;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.SpeedTest.IoC {
   
   internal static class Registrations {

      internal static void NonDisposableTransient(Container container) {
         container.Register<IService, SampleService>(Lifestyle.Transient);
      }
   }
}

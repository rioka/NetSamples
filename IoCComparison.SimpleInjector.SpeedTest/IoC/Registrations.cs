using System;
using System.Linq;
using IoCComparison.Core;
using IoCComparison.SimpleInjector.Addons;
using IoCComparison.SimpleInjector.Addons.Interceptors;
using IoCComparison.SimpleInjector.SpeedTest.Infrastructure;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.SpeedTest.IoC {
   
   internal static class Registrations {

      private static readonly Type[] InterceptableTypes = {
         typeof(IService)
      };

      internal static void NonDisposableTransient(Container container) {
         
         container.Register<ILogger, DebugLogger>(Lifestyle.Singleton);
         container.Register<IService, SampleService>(Lifestyle.Transient);
      }

      internal static void Interceptors(Container container) {

         // Enable interceptor for selected types
         container.InterceptWith<DummyInterceptor>(t => InterceptableTypes.Contains(t));
      }
   }
}

using System;
using System.Diagnostics;
using System.Linq;
using IoCComparison.Core;
using IoCComparison.SimpleInjector.SpeedTest.IoC;

namespace IoCComparison.SimpleInjector.SpeedTest {
   class Program {
      static void Main(string[] args) {

         var count = args.Any() ? Convert.ToInt32(args.First()) : 100;

         var container = ContainerFactory.Build(Registrations.NonDisposableTransient);

         var sw = new Stopwatch();
         sw.Start();
         for (var i = 0; i < count; i++) {
            var instance = container.GetInstance<IService>();
         }
         sw.Stop();

         Console.WriteLine("{0:0,000} instances, {1:0,000} ms", count, sw.ElapsedMilliseconds);
      }
   }
}

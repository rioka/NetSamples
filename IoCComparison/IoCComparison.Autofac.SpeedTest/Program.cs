using System;
using System.Diagnostics;
using System.Linq;
using Autofac;
using IoCComparison.Autofac.SpeedTest.IoC;
using IoCComparison.Autofac.SpeedTest.IoC.Installers;
using IoCComparison.Core;

namespace IoCComparison.Autofac.SpeedTest {
   class Program {
      static void Main(string[] args) {

         var count = args.Any() ? Convert.ToInt32(args.First()) : 100;

         var container = ContainerFactory.Build(new NonDisposableTypeAsTransient(), new Interceptors());

         var sw = new Stopwatch();
         sw.Start();
         for (var i = 0; i < count; i++) {
            var instance = container.Resolve<IService>();
            instance.Run();
         }
         sw.Stop();

         Console.WriteLine("{0:#,##0} instances, {1:0,000} ms", count, sw.ElapsedMilliseconds);
      }
   }
}

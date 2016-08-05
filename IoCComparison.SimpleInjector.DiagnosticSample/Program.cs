using System;
using System.Linq;
using IoCComparison.Core;
using IoCComparison.SimpleInjector.DiagnosticSample.IoC;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace IoCComparison.SimpleInjector.DiagnosticSample {
   class Program {
      static void Main(string[] args) {

         DisposableTypeRegisteredAsTransient();
      }

      private static void DisposableTypeRegisteredAsTransient() {

         var container = new Container();
         // resolve primitive and string parameters via appSettings using Typename.paramName as the key
         container.Options.RegisterParameterConvention(new TypenameArgumentNameParameterConvention());

         container.Register<IService, SampleService>();
         container.Register<ITaskRunner, FakeTaskRunner>();
         container.Register<ISecondService, DisposableSecondService>();

         container.Verify(VerificationOption.VerifyOnly);

         var result = Analyzer.Analyze(container);
         result
            .ToList()
            .ForEach(r => Console.WriteLine("{0}\t{1}\t{2}", r.Severity, r.DiagnosticType, r.Description));
         
         container.Dispose();
      }
   }
}

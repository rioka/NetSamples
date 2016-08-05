using IoCComparison.Core;
using IoCComparison.SimpleInjector.DiagnosticSample.IoC;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace IoCComparison.SimpleInjector.DiagnosticSample {
   class Program {
      static void Main(string[] args) {


         var container = new Container();
         // resolve primitive and string parameters via appSettings using Typename.paramName as the key
         container.Options.RegisterParameterConvention(new TypenameArgumentNameParameterConvention());

         container.Register<IService, SampleService>();
         container.Register<ITaskRunner, FakeTaskRunner>();
         container.Register<ISecondService, DisposableSecondService>();

         container.Verify(VerificationOption.VerifyOnly);

         var result = Analyzer.Analyze(container);

         container.Dispose();
      }
   }
}

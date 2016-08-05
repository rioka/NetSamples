using SimpleInjector;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {
   
   public static class ContainerExtensions {

      internal static void RegisterParameterConvention(this ContainerOptions options, IParameterConvention convention) {

         options.DependencyInjectionBehavior = 
            new ConventionDependencyInjectionBehavior(options.DependencyInjectionBehavior, convention);
      }
   }
}

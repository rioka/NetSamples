using SimpleInjector;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {
   
   public static class ContainerExtensions {

      /// <summary>
      /// Register
      /// </summary>
      /// <param name="options"></param>
      /// <param name="convention"></param>
      internal static void RegisterParameterConvention(this ContainerOptions options, IParameterConvention convention) {

         options.DependencyInjectionBehavior = 
            new ConventionDependencyInjectionBehavior(options.DependencyInjectionBehavior, convention);
      }
   }
}

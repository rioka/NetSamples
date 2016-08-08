using SimpleInjector;

namespace IoCComparison.SimpleInjector.Addons.Behaviors {
   
   public static class ContainerExtensions {

      /// <summary>
      /// Register
      /// </summary>
      /// <param name="options"></param>
      /// <param name="convention"></param>
      public static void RegisterParameterConvention(this ContainerOptions options, IParameterConvention convention) {

         options.DependencyInjectionBehavior = 
            new ConventionDependencyInjectionBehavior(options.DependencyInjectionBehavior, convention);
      }
   }
}

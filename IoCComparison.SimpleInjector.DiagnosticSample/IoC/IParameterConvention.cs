using System;
using System.Linq.Expressions;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {

   /// <summary>
   /// Custom interface for primitive-types parameters injection
   /// </summary>
   internal interface IParameterConvention {

      /// <summary>
      /// Check if a parameter can be resolved using current convention
      /// </summary>
      /// <param name="target">Details for the parameters being resolved</param>
      /// <param name="injectedInto">Type being resolved</param>
      /// <returns>True if the type can be resolved</returns>
      bool CanResolve(InjectionTargetInfo target, Type injectedInto);
      
      /// <summary>
      /// Get the expression to be passed to the constructor
      /// </summary>
      /// <param name="consumer">Consumer (type which needs the parameter)</param>
      /// <returns>The expression for the constructor</returns>
      Expression BuildExpression(InjectionConsumerInfo consumer);
   }
}

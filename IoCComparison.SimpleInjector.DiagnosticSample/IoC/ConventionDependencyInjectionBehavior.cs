using System.Diagnostics;
using System.Linq.Expressions;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {
   
   /// <summary>
   /// Custom dependency injection behavior to allow for injection of primitive types 
   /// based on conventions; actually combines the existing behavior with a
   /// new, more specific, one
   /// </summary>
   /// <remarks>Decorates the initial behavior with the new one</remarks>
   internal class ConventionDependencyInjectionBehavior : IDependencyInjectionBehavior {

      #region Data

      private readonly IDependencyInjectionBehavior _initial;
      private readonly IParameterConvention _convention;

      #endregion

      public ConventionDependencyInjectionBehavior(IDependencyInjectionBehavior initial, IParameterConvention convention) {
         _initial = initial;
         _convention = convention;
      }

      #region IDependencyInjectionBehavior
      
      [DebuggerStepThrough]
      public void Verify(InjectionConsumerInfo consumer) {
         if (!_convention.CanResolve(consumer.Target, consumer.ImplementationType)) {
            _initial.Verify(consumer);
         }
      }

      [DebuggerStepThrough]
      public Expression BuildExpression(InjectionConsumerInfo consumer) {
         return _convention.CanResolve(consumer.Target, consumer.ImplementationType) ? 
            _convention.BuildExpression(consumer) : 
            _initial.BuildExpression(consumer);
      }

      #endregion
   }
}

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {
   
   internal class ConventionDependencyInjectionBehavior : IDependencyInjectionBehavior {
      private readonly IDependencyInjectionBehavior _initial;
      private readonly IParameterConvention _convention;

      public ConventionDependencyInjectionBehavior(IDependencyInjectionBehavior initial, IParameterConvention convention) {
         _initial = initial;
         _convention = convention;
      }

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
   }
}

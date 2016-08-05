using System;
using System.Linq.Expressions;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {

   internal interface IParameterConvention {

      bool CanResolve(InjectionTargetInfo target, Type injectedInto);
      Expression BuildExpression(InjectionConsumerInfo consumer);

   }
}

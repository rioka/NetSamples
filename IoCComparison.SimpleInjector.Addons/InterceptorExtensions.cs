using System;
using System.Linq.Expressions;
using Castle.DynamicProxy;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.Addons {

   public static class InterceptorExtensions {

      private static readonly ProxyGenerator Generator = new ProxyGenerator();

      private static readonly Func<Type, object, IInterceptor, object> CreateProxy =
          (p, t, i) => Generator.CreateInterfaceProxyWithTarget(p, t, i);

      public static void InterceptWith<TInterceptor>(this Container c, Predicate<Type> predicate)
         where TInterceptor : class, IInterceptor {
         
         c.ExpressionBuilt += (s, e) => {
            if (predicate(e.RegisteredServiceType)) {
               var interceptorExpression =
                   c.GetRegistration(typeof(TInterceptor), true).BuildExpression();

               e.Expression = Expression.Convert(
                   Expression.Invoke(Expression.Constant(CreateProxy),
                       Expression.Constant(e.RegisteredServiceType, typeof(Type)),
                       e.Expression,
                       interceptorExpression),
                   e.RegisteredServiceType);
            }
         };
      }
   }
}

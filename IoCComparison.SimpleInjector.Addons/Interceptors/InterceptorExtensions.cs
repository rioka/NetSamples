using System;
using System.Linq.Expressions;
using Castle.DynamicProxy;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.Addons.Interceptors {

   public static class InterceptorExtensions {

      private static readonly ProxyGenerator Generator = new ProxyGenerator();

      private static readonly Func<Type, object, IInterceptor, object> CreateProxy =
          (p, t, i) => Generator.CreateInterfaceProxyWithTarget(p, t, i);

      /// <summary>
      /// Attach an interceptor to selected service types
      /// </summary>
      /// <typeparam name="TInterceptor">Type of the interceptor</typeparam>
      /// <param name="c">Container</param>
      /// <param name="predicate">Predicate on the serviee type</param>
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

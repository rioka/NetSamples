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
      /// <param name="predicate">Predicate to apply the interceptor only to types satisfying a given criteria</param>
      public static void InterceptWith<TInterceptor>(this Container c, Predicate<Type> predicate)
         where TInterceptor : class, IInterceptor {
         
         c.ExpressionBuilt += (s, e) => {
            // if the type is to be intercepted
            if (predicate(e.RegisteredServiceType)) {
               // get the expression to get the interceptor as registered in the container
               var interceptorExpression =
                  c.GetRegistration(typeof (TInterceptor), true).BuildExpression();

               // ... and set the expression for the service to return the 
               // proxy wrapping the original component registered for the service (e.RegisteredServiceType)
               // to sum up
               // Create an expression for CreateProxy, invoke it passing 
               // - the registered type
               // - the expression to get the component registered for that type
               // - the interceptor(s)
               // and finally convert it to an expression to convert the type of the new instance
               // to the registered type
               e.Expression = Expression.Convert(
                  Expression.Invoke(Expression.Constant(CreateProxy),
                     Expression.Constant(e.RegisteredServiceType, typeof (Type)),
                     e.Expression,
                     interceptorExpression),
                  e.RegisteredServiceType);

               // The final result is that when we request the container an instance of an "intercepted" type,
               // it will return an instance of the interceptor, because the original expression for the service
               // has been wrapped with a new expression returning an interceptor
            }
         };
      }
   }
}

using System;
using Castle.DynamicProxy;

namespace IoCComparison.SimpleInjector.Addons {

   public static class Interceptor {

      private static readonly ProxyGenerator Generator = new ProxyGenerator();

      public static object CreateProxy(Type type, IInterceptor interceptor, object target) {

         return Generator.CreateInterfaceProxyWithTarget(type, target, interceptor);
      }
   }
}

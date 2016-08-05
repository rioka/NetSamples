using System.Diagnostics;
using Castle.DynamicProxy;

namespace IoCComparison.SimpleInjector.SpeedTest.Infrastructure {

   internal class DummyInterceptor : IInterceptor {
      
      public void Intercept(IInvocation invocation) {
#if DEBUG
         Debug.WriteLine("Intercepting {0}.{1}", invocation.InvocationTarget.GetType().Name, invocation.GetConcreteMethod().Name);
#endif
         invocation.Proceed();
      }
   }
}

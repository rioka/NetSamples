using System;
using System.Diagnostics;
using System.Reflection;

namespace IoCComparison.Core {

   public class FakeTaskRunner : ITaskRunner {
      
      public bool Run() {
#if DEBUG
         Debug.WriteLine("Executing {0}.{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
#endif
         return DateTime.UtcNow.Millisecond%2 == 0;
      }
   }
}

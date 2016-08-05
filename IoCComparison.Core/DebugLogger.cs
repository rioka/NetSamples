using System.Diagnostics;

namespace IoCComparison.Core {

   public class DebugLogger : ILogger {

      public void Log(string message) {
#if DEBUG
         Debug.WriteLine(message);
#endif
      }
   }
}

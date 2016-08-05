using System.Reflection;

namespace IoCComparison.Core {
   
   public class SampleService : IService {
      
      public ILogger Logger { get; set; }

      public void Run() {
         Logger.Log(string.Format("Within {0}.{1}", GetType().Name, MethodBase.GetCurrentMethod().Name));
      }
   }
}

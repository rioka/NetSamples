using System.Reflection;

namespace IoCComparison.Core {
   
   public class SampleService : IService {
      
      public ILogger Logger { get; set; }

      public void Run() {
         Logger.Log("Within " + MethodBase.GetCurrentMethod().Name);
      }
   }
}

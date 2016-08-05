using IoCComparison.Core;
using IoCComparison.WcfServices.Models;

namespace IoCComparison.WcfServices {
   
   public class BarService : IBarService {
      
      private readonly ITaskRunner _taskRunner;

      public BarService(ITaskRunner taskRunner) {
         _taskRunner = taskRunner;
      }

      public BarResponse Execute(BarRequest request) {

         var result = _taskRunner.Run();
         return new BarResponse() {
            Code = result ? "Ok" : "Error"
         };
      }
   }
}
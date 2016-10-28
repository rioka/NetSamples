using System.Diagnostics;
using System.Reflection;

namespace IoCComparison.Core {

   public class TransactionalTaskRunnerDecorator : TaskRunnerDecorator {
      
      public TransactionalTaskRunnerDecorator(ITaskRunner taskRunner) : base(taskRunner) {}

      public override bool Run() {
#if DEBUG
         Debug.WriteLine("Executing {0}.{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
#endif
         return base.Run();
      }
   }
}

using System.Reflection;

namespace IoCComparison.Core {

   public class LoggingTaskRunnerDecorator : TaskRunnerDecorator {
      
      private readonly ILogger _logger;

      public LoggingTaskRunnerDecorator(ITaskRunner taskRunner, ILogger logger) : base(taskRunner) {
         _logger = logger;
      }

      public override bool Run() {
         _logger.Log("Executing " + GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
         return base.Run();
      }
   }
}

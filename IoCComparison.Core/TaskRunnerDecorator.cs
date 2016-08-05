
namespace IoCComparison.Core {
   
   public class TaskRunnerDecorator : ITaskRunner {

      private readonly ITaskRunner _taskRunner;

      public TaskRunnerDecorator(ITaskRunner taskRunner) {
         _taskRunner = taskRunner;
      }

      public virtual bool Run() {
         return _taskRunner.Run();
      }
   }
}

using System;
using System.Diagnostics;
using System.Reflection;

namespace IoCComparison.Core {

   public class DisposableSecondService : ISecondService, IDisposable {
      
      private readonly ITaskRunner _taskRunner;
      private readonly int _maxTask;

      public DisposableSecondService(ITaskRunner taskRunner, int maxTask) {
         _taskRunner = taskRunner;
         _maxTask = maxTask;
      }

      public void Dispose() {
#if DEBUG
         Debug.WriteLine("Disposing {0}", GetType());
#endif
      }

      public void Execute() {
#if DEBUG
         Debug.WriteLine("Running {0}.{1}", GetType().Name, MethodBase.GetCurrentMethod().Name);
#endif
      }
   }
}
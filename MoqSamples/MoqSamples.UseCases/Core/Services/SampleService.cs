using System;
using System.Linq.Expressions;

namespace MoqSamples.UseCases.Core.Services {

   public interface IRunner {

      int Run(Expression<Func<int, int>> exp);
   }

   public class SampleService {

      private readonly IRunner _runner;

      public SampleService(IRunner runner) {

         _runner = runner;
      }

      public int Do(int i) {

         return _runner.Run(x => x + 2);
      }

      public int DoOther(int i) {

         return _runner.Run(x => x + 3);
      }
   }
}
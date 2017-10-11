using System.Diagnostics;

namespace AutofacSamples.Scenarios.Core.FunctionsAsFactories {

   public interface ILineProcessor {

      void Process();
   }

   public class LineProcessor : ILineProcessor {

      public LineProcessor(string s) {  }

      #region ILineProcessor

      public void Process() {

         Debug.WriteLine("Processing...");
      }

      #endregion
   }
}
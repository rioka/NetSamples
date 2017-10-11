using System;

namespace AutofacSamples.Scenarios.Core.FunctionsAsFactories {

   public interface IFileReader {

      void Process(string s);
   }

   public class FileReader : IFileReader {

      private readonly Func<string, ILineProcessor> _processorFactory;

      #region Constructors

      public FileReader(Func<string, ILineProcessor> processorFactory) {

         _processorFactory = processorFactory;
      }

      #endregion

      #region IFileReader

      public void Process(string s) {

         var processor = _processorFactory(s);
         processor.Process();
      }
      
      #endregion
   }
}
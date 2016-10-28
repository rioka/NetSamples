
using System;
using System.Linq;

namespace UsingFactories.Core.FactoryDelegate {
   public class Translator : ITranslator {
      
      private readonly int _maxErrors;

      public Translator(int maxErrors) {
      
         _maxErrors = maxErrors;
      }

      public string Translate(string source) {

         return new string(source.Reverse().ToArray());
      }
   }
}

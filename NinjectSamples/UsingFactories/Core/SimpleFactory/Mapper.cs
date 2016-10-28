
using System;
using System.Diagnostics;

namespace UsingFactories.Core.SimpleFactory {
   
   /// <summary>
   /// Resolved via a factory
   /// </summary>
   public class Mapper : IMapper, IDisposable {
      
      private readonly int _threshold;

      public Mapper(int threshold) {

         _threshold = threshold;
      }

      public void Dispose() {
         Debug.WriteLine("Mapper.Dispose");
      }
   }
}

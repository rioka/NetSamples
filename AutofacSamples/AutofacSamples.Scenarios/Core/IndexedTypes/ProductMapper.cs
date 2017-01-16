using System.Diagnostics;
using AutofacSamples.Scenarios.Core.Models;

namespace AutofacSamples.Scenarios.Core.IndexedTypes {

   public class ProductMapper : BaseMapper<Product> {

      public ProductMapper() {
         Debug.WriteLine("ProductMapper .ctor");
      }

      public override Product Map(string source) {

         return new Product();
      }
   }
}

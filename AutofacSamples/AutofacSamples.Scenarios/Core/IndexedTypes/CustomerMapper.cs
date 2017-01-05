using System;
using System.Diagnostics;
using AutofacSamples.Scenarios.Core.Models;

namespace AutofacSamples.Scenarios.Core.IndexedTypes {

   /// <summary>
   /// Map a <see cref="Customer"/>
   /// </summary>
   public class CustomerMapper : BaseMapper<Customer> {

      public CustomerMapper() {

         Debug.WriteLine("CustomerMapper .ctor");
      }

      public override Customer Map(string source) {
         throw new NotImplementedException();
      }
   }
}

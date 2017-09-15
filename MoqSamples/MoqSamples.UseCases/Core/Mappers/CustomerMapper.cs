using MoqSamples.UseCases.Core.Models;

namespace MoqSamples.UseCases.Core.Mappers {

   public class CustomerMapper : BaseMapper<Customer> {

      public override Customer Map(string source) {

         return new Customer();
      }
   }
}
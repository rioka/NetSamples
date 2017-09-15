using System.Collections.Generic;
using MoqSamples.UseCases.Core.Models;

namespace MoqSamples.UseCases.Core.Services {

   public interface ICustomerService {

      int Update(IEnumerable<Customer> customers);
   }

   public class CustomerService : ICustomerService {

      private readonly IClient _client;

      public CustomerService(IClient client) {

         _client = client;
      }

      public int Update(IEnumerable<Customer> customers) {

         var failed = 0;
         foreach (var customer in customers) {
            if (!_client.Connect()) {
               ++failed;
            }
            else {
               DoSomething(customer);
            }
         }
         return failed;
      }

      private void DoSomething(Customer customer) {

         return;
      }
   }
}
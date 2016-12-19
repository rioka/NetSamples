using AutoFixtureSamples.Core.Models;

namespace AutoFixtureSamples.Core {

   public interface ICustomerService {

      Customer Get(int id);
   }

   public class CustomerService : ICustomerService {

      ICustomerRepository _repository;

      public CustomerService(ICustomerRepository repository) {

         _repository = repository;
      }

      public Customer Get(int id) {

         return _repository.Get(id);
      }
   }
}

using System;
using System.Collections.Generic;
using AutoFixtureSamples.Core.Models;

namespace AutoFixtureSamples.Core {

   public interface ICustomerRepository {

      Customer Get(int id);

      IEnumerable<Customer> GetAll();
   }

   public class CustomerRepository : ICustomerRepository {

      public Customer Get(int id) {

         throw new NotImplementedException();
      }

      public IEnumerable<Customer> GetAll() {

         throw new NotImplementedException();
      }
   }
}

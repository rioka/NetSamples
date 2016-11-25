using AutofacSamples.Scenarios.Core.Models;

namespace AutofacSamples.Scenarios.Core.ClosedGenerics {
   
   public interface IRepository<out T> {

      #region Apis

      T Get(int id);

      #endregion
   }

   public class CustomerRepository : IRepository<Customer> {

      #region IRepository

      public Customer Get(int id) {
         return new Customer() {
            Id = id
         };
      }

      #endregion
   }
}

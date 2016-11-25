using AutofacSamples.Scenarios.Core.Models;

namespace AutofacSamples.Scenarios.Core.Repositories {
   
   public interface IRepository {

      #region Apis

      Customer Get(int id);

      #endregion
   }

   public class Repository : IRepository {

      #region IRepository

      public Customer Get(int id) {

         return new Customer() {
            Id = id
         };
      }

      #endregion
   }
}

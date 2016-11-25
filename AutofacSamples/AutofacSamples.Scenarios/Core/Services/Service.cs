using AutofacSamples.Scenarios.Core.Repositories;

namespace AutofacSamples.Scenarios.Core.Services {
   
   public interface IService {

      #region Apis

      void Execute();

      #endregion
   }

   public class Service : IService {
      
      private readonly IRepository _repository;

      #region Constructors

      public Service(IRepository repository) {
         
         _repository = repository;
      }

      #endregion

      #region IService

      public void Execute() {}

      #endregion
   }
}

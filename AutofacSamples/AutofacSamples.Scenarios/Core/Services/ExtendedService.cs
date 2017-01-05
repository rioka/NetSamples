using AutofacSamples.Scenarios.Core.Repositories;

namespace AutofacSamples.Scenarios.Core.Services {
   
   public interface IExtendedService {
      
      #region Apis
      
      int Retries { get; }

      void Execute();

      #endregion
   }

   /// <summary>
   /// Sample service with a dependencies on a runtime value and a type provided Autofac
   /// </summary>
   public class ExtendedService : IExtendedService {
      
      private readonly IRepository _repository;

      #region Constructors

      public ExtendedService(int retries, IRepository repository) {
         
         Retries = retries;
         _repository = repository;
      }

      #endregion

      #region IExtendedService

      public int Retries { get; private set; }

      public void Execute() {}

      #endregion
   }
}

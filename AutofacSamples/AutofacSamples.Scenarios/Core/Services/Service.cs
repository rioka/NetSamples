namespace AutofacSamples.Scenarios.Core.Services {
   
   public interface IService {

      #region Apis

      void Execute();

      #endregion

   }

   public class Service : IService {
      
      #region IService

      public void Execute() {}

      #endregion
   }
}

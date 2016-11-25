
namespace AutofacSamples.Scenarios.Core.Handlers {

   public interface IHandler {
      
      #region Apis

      void Handle();

      #endregion
   }

   public class Handler : IHandler {

      #region IHandler

      public void Handle() {}

      #endregion
   }
}

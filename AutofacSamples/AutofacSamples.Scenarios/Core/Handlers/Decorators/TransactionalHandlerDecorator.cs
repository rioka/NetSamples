namespace AutofacSamples.Scenarios.Core.Handlers.Decorators {
   
   public class TransactionalHandlerDecorator : HandleDecorator {
      
      #region Constructors

      public TransactionalHandlerDecorator(IHandler handler) : base(handler) { }

      #endregion

      #region Overrides

      public override void Handle() {

         StartTransaction();
         base.Handle();
         Commit();
      }

      #endregion

      #region Internals

      private void StartTransaction() {
      }

      private void Commit() {
      }

      #endregion
   }
}

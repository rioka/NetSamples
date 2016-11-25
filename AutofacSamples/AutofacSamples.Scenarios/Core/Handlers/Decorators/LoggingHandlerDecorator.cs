namespace AutofacSamples.Scenarios.Core.Handlers.Decorators {
   
   public class LoggingHandlerDecorator : HandleDecorator {

      #region Constructors

      public LoggingHandlerDecorator(IHandler handler) : base(handler) { }

      #endregion

      #region Overrides

      public override void Handle() {
         Log();
         base.Handle();
      }

      #endregion

      #region Internals

      private void Log() {}

      #endregion
   }
}

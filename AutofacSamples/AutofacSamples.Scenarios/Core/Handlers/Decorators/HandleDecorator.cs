using System;

namespace AutofacSamples.Scenarios.Core.Handlers.Decorators {
   
   public class HandleDecorator : IHandler {
      
      private readonly IHandler _handler;

      #region Constructors

      protected HandleDecorator(IHandler handler) {
         _handler = handler;
      }

      #endregion
      
      #region IHandler

      public virtual void Handle() {
         
         _handler.Handle();
      }

      #endregion
   }
}

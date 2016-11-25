using System;

namespace AutofacSamples.Scenarios.Core.Services {

   public interface IParser<out T> {

      #region Apis

      T Parse(string value);

      #endregion
   }

   public class Parser<T> : IParser<T> {

      #region IParser

      public T Parse(string value) {

         throw new NotImplementedException();
      }

      #endregion
   }
}

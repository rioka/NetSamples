using System;

namespace UsingFactories.Core.FunctionFactory {

   /// <summary>
   /// A factory for IPopulator will be created by the container from the populatorFactory argument to the constructor
   /// </summary>
   public class Builder {
      
      private readonly Func<string, IPopulator> _populatorFactory;

      public Builder(Func<string, IPopulator> populatorFactory) {
         
         _populatorFactory = populatorFactory;
      }

      public int Run(string name) {

         var populator = _populatorFactory(name);

         return name.Length;
      }
   }
}

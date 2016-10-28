
namespace UsingFactories.Core.ExtendedFactory {
   
   // Instantiated via a factory, which provides also a proper IStrategy instance
   public class Resolver : IResolver {
      
      private readonly IStrategy _strategy;
      private readonly int _retries;

      public Resolver(IStrategy strategy, int retries) {
      
         _strategy = strategy;
         _retries = retries;
      }
   }
}

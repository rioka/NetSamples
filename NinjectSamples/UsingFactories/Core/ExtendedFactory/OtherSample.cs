
namespace UsingFactories.Core.ExtendedFactory {
   public class OtherSample {
      
      private readonly IResolverFactory _factory;

      public OtherSample(IResolverFactory factory) {
         _factory = factory;
      }

      public void Run(int tries) {

         var resolver = _factory.Create(tries);
      }
   }
}

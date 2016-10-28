
namespace UsingFactories.Core.SimpleFactory {
   
   public class Sample {
      
      private readonly IMapperFactory _factory;

      public Sample(IMapperFactory factory) {

         _factory = factory;
      }

      public void Run(int value) {

         var mapper = _factory.Create(value);
      }
   }
}

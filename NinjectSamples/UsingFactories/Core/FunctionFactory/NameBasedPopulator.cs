
namespace UsingFactories.Core.FunctionFactory {
   
   /// <summary>
   /// Instantiated via a factory dinamycally created from a function in the constructor of Builder
   /// </summary>
   public class NameBasedPopulator : IPopulator {
      
      private readonly IConfigRepository _configRepository;
      private readonly string _userName;

      public NameBasedPopulator(IConfigRepository configRepository, string userName) {
         
         _configRepository = configRepository;
         _userName = userName;
      }
   }
}

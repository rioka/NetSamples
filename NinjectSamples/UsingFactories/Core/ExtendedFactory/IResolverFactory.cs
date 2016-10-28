
namespace UsingFactories.Core.ExtendedFactory {
   
   public interface IResolverFactory {

      IResolver Create(int retries);
   }
}

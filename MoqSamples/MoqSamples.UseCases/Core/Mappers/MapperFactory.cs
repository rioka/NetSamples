namespace MoqSamples.UseCases.Core.Mappers {

   public interface IMapperFactory {

      IMapper Get(string type);
   }

   public class MapperFactory {

      public IMapper Get(string type) {
         return new CustomerMapper();
      }
   }
}

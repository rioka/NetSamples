namespace MoqSamples.UseCases.Core.Mappers {

   public interface IMapper { }

   public abstract class BaseMapper<T> : IMapper {

      public abstract T Map(string source);
   }
}

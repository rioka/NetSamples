namespace MoqSamples.UseCases.Core.Mappers {
   public class MultiTypeMapper {

      private readonly IMapperFactory _factory;

      public MultiTypeMapper(IMapperFactory factory) {

         _factory = factory;
      }

      public void MapFrom(string source) {

         dynamic mapper = _factory.Get(source);

         var instance = mapper.Map(source);

         // do something with instance...
      }

      /// <summary>
      /// Just a placeholder
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      string GetNameForType(string s) {
         return s + "Mapper";
      }
   }
}

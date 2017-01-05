using Autofac.Features.Indexed;

namespace AutofacSamples.Scenarios.Core.IndexedTypes {

   /// <summary>
   /// Factory to return the proper mapper for the requested target type
   /// </summary>
   public class MapperFactory  {

      /// <summary>
      /// Using <see cref="IMapper"/> since cannot define 
      /// <code>
      /// IIndex&lt;string, BaseMapper&lt;&gt;&gt;
      /// </code>
      /// </summary>
      private readonly IIndex<string, IMapper> _mappers;

      #region Constructors

      public MapperFactory(IIndex<string, IMapper> mappers) {

         _mappers = mappers;
      }

      #endregion

      #region Apis

      /// <summary>
      /// Get a mapper for the requested type
      /// </summary>
      /// <typeparam name="T">Type the mapper will return</typeparam>
      /// <returns>A proper <see cref="BaseMapper{T}"/> implementation for the requested type</returns>
      public BaseMapper<T> GetMapper<T>() {

         return GetMapper(typeof(T).Name) as BaseMapper<T>;
      }

      /// <summary>
      /// Get a mapper for the requested type
      /// </summary>
      /// <param name="typeName">Name of the type the mapper will return</param>
      /// <returns>A proper <see cref="BaseMapper{T}"/> implementation for the requested type, as <see cref="IMapper"/>
      /// since we do not have enough iomformation to case to the specific mapper type</returns>
      public IMapper GetMapper(string typeName) {

         return _mappers[typeName] ;
      }

      #endregion
   }
}

namespace AutofacSamples.Scenarios.Core.IndexedTypes {

   /// <summary>
   /// Marker interface
   /// </summary>
   public interface IMapper { }

   /// <summary>
   /// Base mapper class
   /// </summary>
   /// <typeparam name="T">Type source content will be mapper to</typeparam>
   public abstract class BaseMapper<T> : IMapper {

      public abstract T Map(string source);
   }
}

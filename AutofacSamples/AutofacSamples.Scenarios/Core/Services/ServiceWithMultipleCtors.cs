using AutofacSamples.Scenarios.Core.IndexedTypes;
using AutofacSamples.Scenarios.Core.Repositories;

namespace AutofacSamples.Scenarios.Core.Services {

   public interface IServiceWithMultipleCtors {
      
      IRepository Repository { get; }

      ProductMapper Mapper{ get; }
   }

   public class ServiceWithMultipleCtors : IServiceWithMultipleCtors {

      private readonly ProductMapper _mapper;
      private readonly IRepository _repository;

      public IRepository Repository {
         get { return _repository; }
      }

      public ProductMapper Mapper {
         get { return _mapper; } 
      }

      public ServiceWithMultipleCtors(IRepository repository) {

         _repository = repository;
      }

      public ServiceWithMultipleCtors(ProductMapper mapper) {
         _mapper = mapper;
      }
   }
}

using Autofac.Features.AttributeFilters;

namespace AutofacSamples.Scenarios.Core.ResolvingWithAttributes {

   public class Importer {

      private readonly IMapper _mapper;

      public IMapper Mapper { get { return _mapper; } }

      public Importer([KeyFilter("simple")] IMapper mapper) {

         _mapper = mapper;
      }
   }
}
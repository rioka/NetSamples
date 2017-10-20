using Autofac.Extras.AttributeMetadata;
using Autofac.Features.AttributeFilters;

namespace AutofacSamples.Scenarios.Core.ResolvingWithAttributes {
   public class Processor {

      private readonly IMapper _mapper;

      public IMapper Mapper { get { return _mapper; } }

      public Processor([KeyFilter("complex")] IMapper mapper) {

         _mapper = mapper;
      }
   }
}
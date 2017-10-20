using Autofac.Features.AttributeFilters;

namespace AutofacSamples.Scenarios.Core.ResolvingWithMetadata {

   public class DdsProcessor {

      private readonly IFailureResolver _resolver;

      public IFailureResolver Resolver { get { return _resolver; } }

      public DdsProcessor([KeyFilter(FinanceSystem.Dds)] IFailureResolver resolver) {

         _resolver = resolver;
      }
   }

   public class SbmsProcessor {

      private readonly IFailureResolver _resolver;

      public IFailureResolver Resolver { get { return _resolver; } }

      public SbmsProcessor([KeyFilter(FinanceSystem.Sbms)] IFailureResolver resolver) {

         _resolver = resolver;
      }
   }
}
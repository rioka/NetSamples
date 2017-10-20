namespace AutofacSamples.Scenarios.Core.ResolvingWithMetadata {

   public interface IFailureResolver {  }

   [FinanceSystem(FinanceSystem.Dds)]
   public class DdsFailureResolver : IFailureResolver { }

   [FinanceSystem(FinanceSystem.Sbms, FinanceSystem.Opale)]
   public class DefaultFailureResolver : IFailureResolver { }
}
using AutofacSamples.Scenarios.Core.Services;

namespace AutofacSamples.Scenarios.Core.Factories {
   
   public class FactoryDelegates {

      public delegate IService ServiceFactory();

      public delegate IExtendedService ExtendedServiceFactory(int retries);
   }
}

using System.ServiceModel;
using IoCComparison.WcfServices.Models;

namespace IoCComparison.WcfServices {

   [ServiceContract]
   public interface IFooService {

      [OperationContract]
      FooResponse GetData(FooRequest request);
   }
}

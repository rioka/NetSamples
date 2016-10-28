using System.ServiceModel;
using IoCComparison.WcfServices.Models;

namespace IoCComparison.WcfServices {

   [ServiceContract]
   public interface IBarService {

      [OperationContract]
      BarResponse Execute(BarRequest request);
   }
}
using System;
using System.Diagnostics;
using IoCComparison.Core;
using IoCComparison.WcfServices.Models;

namespace IoCComparison.WcfServices {
   
   public class FooService : IFooService, IDisposable {

      private readonly Lazy<IService> _service;
      private readonly ISecondService _secondService;

      private IService Service { get { return _service.Value;} }

      public FooService(Lazy<IService> service, ISecondService secondService) {
         _service = service;
         _secondService = secondService;
      }

      public FooResponse GetData(FooRequest request) {

         return new FooResponse() {
            Code = "OK",
            Message = string.Format("{0} is {1} (at {2})", request.Name, request.Age, DateTime.UtcNow)
         };
      }

      public void Dispose() {
#if DEBUG
         Trace.WriteLine("Disposing {0}" + GetType());
#endif
      }
   }
}
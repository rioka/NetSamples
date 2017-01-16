using System.Data.Common;

namespace AutofacSamples.Scenarios.Core.Services {

   public interface IServiceWithOptionalParamsInCtor {

      DbConnection Connection { get; }
   }

   public class ServiceWithOptionalParamsInCtor : IServiceWithOptionalParamsInCtor {

      private readonly IService _service;
      private readonly DbConnection _connection;

      public DbConnection Connection {
         get { return _connection; }
      }

      #region Constructors

      public ServiceWithOptionalParamsInCtor(IService service, DbConnection connection = null) {

         _service = service;
         _connection = connection;
      }

      #endregion
   }
}
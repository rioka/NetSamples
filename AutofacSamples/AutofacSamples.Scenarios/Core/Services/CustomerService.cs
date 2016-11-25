
namespace AutofacSamples.Scenarios.Core.Services {

   public interface IServiceBase { }

   public interface IUIService : IServiceBase { }

   public abstract class ServiceBase : IUIService { }

   public class CustomerService : ServiceBase { }
}

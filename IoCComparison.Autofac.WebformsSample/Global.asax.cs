using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using IoCComparison.Core;

namespace IoCComparison.Autofac.WebformsSample {
   public class Global : HttpApplication, IContainerProviderAccessor {

      // Provider that holds the application container.
      static IContainerProvider _containerProvider;
      
      void Application_Start(object sender, EventArgs e) {
         
         // Code that runs on application startup
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);

         SetupContainer();
      }

      #region IContainerProviderAccessor
      
      // Instance property that will be used by Autofac HttpModules
      // to resolve and inject dependencies.
      public IContainerProvider ContainerProvider {
         get { return _containerProvider; }
      }

      #endregion

      #region Internals

      private void SetupContainer() {
         // Build up your application container and register your dependencies.
         var builder = new ContainerBuilder();

         builder
            .RegisterType<DebugLogger>()
            .AsImplementedInterfaces()
            .SingleInstance();

         builder
            .RegisterType<SampleService>()
            .As<IService>()
            .PropertiesAutowired()                    // populate Logger property on SampleService
            .InstancePerRequest();

         _containerProvider = new ContainerProvider(builder.Build());
      }

      #endregion
   }
}
using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Web;
using IoCComparison.Autofac.MixedWebformsMvcSample.Controllers;
using IoCComparison.Core;

namespace IoCComparison.Autofac.MixedWebformsMvcSample {

   public partial class Global : IContainerProviderAccessor {

      // Provider that holds the application container.
      static IContainerProvider _containerProvider;

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

         // Register MVC controllers
         // beware: global.asax is in its own assembly
         builder.RegisterControllers(Assembly.GetAssembly(typeof(ValueController)));
         // optional: enable property injection in view
         builder.RegisterSource(new ViewRegistrationSource());

         var container = builder.Build();
         _containerProvider = new ContainerProvider(container);

         // set Autofac as dependency resolver for Mvc
         DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
      }

      #endregion
   }
}
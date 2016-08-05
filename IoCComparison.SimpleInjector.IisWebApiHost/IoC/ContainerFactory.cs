using System;
using System.Configuration;
using System.Web.Http;
using IoCComparison.Core;
using IoCComparison.WebApi.Controllers;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace IoCComparison.SimpleInjector.IisWebApiHost.IoC {

   public static class ContainerFactory {

      public static Container Build(HttpConfiguration config) {

         var container = new Container();

         container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

         container.RegisterWebApiControllers(config, typeof(FooController).Assembly);
         container.Register<IService, SampleService>(Lifestyle.Scoped);
         container.Register<Lazy<IService>>(() => new Lazy<IService>(container.GetInstance<IService>));
         container.Register<ISecondService>(() => 
            new DisposableSecondService(
               container.GetInstance<ITaskRunner>(), 
               Convert.ToInt32(ConfigurationManager.AppSettings["DisposableSecondService.maxTask"])), 
            Lifestyle.Scoped);

         container.Register<ILogger, DebugLogger>(Lifestyle.Scoped);
         container.Register<ITaskRunner, FakeTaskRunner>(Lifestyle.Scoped);
         container.RegisterDecorator<ITaskRunner, LoggingTaskRunnerDecorator>(Lifestyle.Scoped);
         container.RegisterDecorator<ITaskRunner, TransactionalTaskRunnerDecorator>(Lifestyle.Scoped);

         config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

         // NOTE FooController inherits IDisposable from ApiController, but .RegisterWebApiControllers
         // update the registration suppressing the warning ("IDisposable registered as transient...")
         // since Web API itself register the controller for disposal at the end of the request
         // and it gets actually disposed (see Trace.WriteLine in FooController.Dispose(...)
         container.Verify();

         return container;
      }
   }
}
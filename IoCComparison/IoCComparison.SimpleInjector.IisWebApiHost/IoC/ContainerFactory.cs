﻿using System;
using System.Web.Http;
using IoCComparison.Core;
using IoCComparison.SimpleInjector.Addons.Behaviors;
using IoCComparison.WebApi.Controllers;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace IoCComparison.SimpleInjector.IisWebApiHost.IoC {

   public static class ContainerFactory {

      public static Container Build(HttpConfiguration config) {

         var container = new Container();

         container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
         // resolve primitive and string parameters via appSettings using Typename.paramName as the key
         container.Options.RegisterParameterConvention(new TypenameArgumentNameParameterConvention());

         container.RegisterWebApiControllers(config, typeof(FooController).Assembly);
         container.Register<IService, SampleService>(Lifestyle.Scoped);
         container.Register<Lazy<IService>>(() => new Lazy<IService>(container.GetInstance<IService>));
         container.Register<ISecondService, DisposableSecondService>(Lifestyle.Scoped);

         container.Register<ILogger, DebugLogger>(Lifestyle.Scoped);
         container.Register<ITaskRunner, FakeTaskRunner>(Lifestyle.Scoped);
         container.RegisterDecorator<ITaskRunner, LoggingTaskRunnerDecorator>(Lifestyle.Scoped);
         container.RegisterDecorator<ITaskRunner, TransactionalTaskRunnerDecorator>(Lifestyle.Scoped);

         config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

         // NOTE Foo/Bar Controllers inherits IDisposable from ApiController, but .RegisterWebApiControllers
         // update the registration suppressing the warning ("IDisposable registered as transient...")
         // since Web API itself register the controller for disposal at the end of the request
         // and it gets actually disposed (see Debug.WriteLine in FooController.Dispose(...)
         container.Verify();

         return container;
      }
   }
}
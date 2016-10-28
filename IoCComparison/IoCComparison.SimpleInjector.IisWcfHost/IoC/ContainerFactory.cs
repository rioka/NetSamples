using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Reflection;
using IoCComparison.Core;
using IoCComparison.SimpleInjector.Addons.Behaviors;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Wcf;

namespace IoCComparison.SimpleInjector.IisWcfHost.IoC {

   public static class ContainerFactory {

      public static Container Build() {

         var container = new Container();
         container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();
         // Override defualt behavior to inject properties
         container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
         // resolve primitive and string parameters via appSettings using Typename.paramName as the key
         container.Options.RegisterParameterConvention(new TypenameArgumentNameParameterConvention());

         container.Register<IService, SampleService>(Lifestyle.Scoped);
         // NOTE this will not be analyzed y .Verify / Analyze
         container.Register<Lazy<IService>>(() => new Lazy<IService>(container.GetInstance<IService>));
         container.Register<ISecondService, DisposableSecondService>(Lifestyle.Scoped);
         container.Register<ILogger, DebugLogger>(Lifestyle.Scoped);
         container.Register<ITaskRunner, FakeTaskRunner>(Lifestyle.Scoped);
         // Registering ITaskRunner decorators...
         // Decorators are applied in the order in which they are registered, so 
         // FakeTaskRunner is wrapped by 
         //    LoggingTaskRunnerDecorator, which is wrapped by wraps 
         //       TransactionalTaskRunnerDecorator 
         container.RegisterDecorator<ITaskRunner, LoggingTaskRunnerDecorator>(Lifestyle.Scoped);
         container.RegisterDecorator<ITaskRunner, TransactionalTaskRunnerDecorator>(Lifestyle.Scoped);

         //container.RegisterWcfServices(typeof(FooService).Assembly);

         // Fails when FooService implements IDisposable if I call .RegisterWcfServices
         // but given the factory in .svc file, this is not needed
         // anyway, it seems service instances do not get disposed
         // No trace from FooService.Dispose()
         // Needs further inspection
         // TODO what is FooService indeed implements IDisposable?
         container.Verify();

         SimpleInjectorServiceHostFactory.SetContainer(container);
         return container;
      }
   }

   #region Internals
   
   /// <summary>
   /// Inject properties marked with <see cref="ImportAttribute"/>
   /// </summary>
   class ImportPropertySelectionBehavior : IPropertySelectionBehavior {

      public bool SelectProperty(Type type, PropertyInfo prop) {
         return prop.GetCustomAttributes(typeof(ImportAttribute)).Any();
      }
   }

   #endregion
}
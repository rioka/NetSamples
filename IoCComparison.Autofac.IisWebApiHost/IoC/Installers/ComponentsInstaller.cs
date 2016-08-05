using System;
using System.Configuration;
using Autofac;
using IoCComparison.Core;

namespace IoCComparison.Autofac.IisWebApiHost.IoC.Installers {
   
   public class ComponentsInstaller : Module {
      
      protected override void Load(ContainerBuilder builder) {

         builder
            .RegisterType<SampleService>()
            .As<IService>()
            .PropertiesAutowired()
            .InstancePerRequest();

         builder
            .RegisterType<DebugLogger>()
            .As<ILogger>()
            .InstancePerRequest();

         // Could be also 
         //   .WithParameter(new TypedParameter(typeof(int), Convert.ToInt32(ConfigurationManager.AppSettings["DisposableSecondService.maxTask"])))
         // given that ctor has only one parameter of type int
         builder
            .RegisterType<DisposableSecondService>()
            .As<ISecondService>()
            .WithParameter("maxTask", Convert.ToInt32(ConfigurationManager.AppSettings["DisposableSecondService.maxTask"]))
            .InstancePerRequest();

         builder
            .RegisterType<DebugLogger>()
            .As<ILogger>()
            .InstancePerRequest();

         // register FakeTaskRunner as ITaskRunner, assigning the name "taskrunner" to this component...
         builder
            .RegisterType<FakeTaskRunner>()
            .Named<ITaskRunner>("taskrunner")
            .InstancePerRequest();

         // ... and then get LoggingTaskRunnerDecorator applied when resolving an ITaskRunner, using the registration named "taskrunner" for the inner component, 
         // and assigning the name "loggingtaskrunner" to this component...
         builder
            .RegisterDecorator<ITaskRunner>((ctx, inner) => new LoggingTaskRunnerDecorator(inner, ctx.Resolve<ILogger>()), "taskrunner")
            .InstancePerRequest()
            .Named<ITaskRunner>("loggingtaskrunner");

         // ... and then get TransactionalTaskRunnerDecorator applied when resolving an ITaskRunner, using the registration named "loggingtaskrunner" for the inner component
         builder
            .RegisterDecorator<ITaskRunner>((ctx, inner) => new TransactionalTaskRunnerDecorator(inner), "loggingtaskrunner")
            .InstancePerRequest();

         // Alternatively, register by convention
         // Get all concrete types from a list of assemblies, implementing at least one interface,
         // and register them against the miplemented interfaces
         //builder
         //   .RegisterAssemblyTypes(typeof(SampleService).Assembly)
         //   .Where(t => t.GetInterfaces().Any()
         //               && !t.IsAbstract
         //               && !t.IsInterface)
         //   .AsImplementedInterfaces()
         //   .InstancePerRequest();
      }
   }
}
using System;
using System.Configuration;
using System.Linq;
using Autofac;
using IoCComparison.Core;

namespace IoCComparison.Autofac.IisWcfHost.IoC.Installers {
   
   public class ComponentsInstaller : Module {
      
      protected override void Load(global::Autofac.ContainerBuilder builder) {

         //builder
         //   .RegisterType<SampleService>()
         //   .As<IService>()
         //   .InstancePerLifetimeScope();

         //builder
         //   .RegisterType<DisposableSecondService>()
         //   .As<ISecondService>()
         //   .InstancePerLifetimeScope();

         // Alternatively, register by convention
         // Get all concrete types from a list of assemblies, implementing at least one interface,
         // and register them against the miplemented interfaces
         builder
            .RegisterAssemblyTypes(typeof(SampleService).Assembly)
            //.RegisterAssemblyTypes(BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray())
            .Where(t => t.GetInterfaces().Any()
                        && !t.IsAbstract
                        && !t.IsInterface
                        && t != typeof(DisposableSecondService)
                        && !t.IsAssignableTo<ITaskRunner>())
            .AsImplementedInterfaces()
            .PropertiesAutowired()
            .InstancePerLifetimeScope();

         builder
            .RegisterType<DisposableSecondService>()
            .As<ISecondService>()
            .WithParameter("maxTask", Convert.ToInt32(ConfigurationManager.AppSettings["DisposableSecondService.maxTask"]))
            .InstancePerLifetimeScope();

         // register FakeTaskRunner as ITaskRunner, assigning the name "taskrunner" to this component...
         builder
            .RegisterType<FakeTaskRunner>()
            .Named<ITaskRunner>("taskrunner")
            .InstancePerLifetimeScope();

         // ... and then get LoggingTaskRunnerDecorator applied when resolving an ITaskRunner, using the registration named "taskrunner" for the inner component, 
         // and assigning the name "loggingtaskrunner" to this component...
         builder
            .RegisterDecorator<ITaskRunner>((ctx, inner) => new LoggingTaskRunnerDecorator(inner, ctx.Resolve<ILogger>()), "taskrunner")
            .InstancePerLifetimeScope()
            .Named<ITaskRunner>("loggingtaskrunner");

         // ... and then get TransactionalTaskRunnerDecorator applied when resolving an ITaskRunner, using the registration named "loggingtaskrunner" for the inner component
         builder
            .RegisterDecorator<ITaskRunner>((ctx, inner) => new TransactionalTaskRunnerDecorator(inner), "loggingtaskrunner")
            .InstancePerLifetimeScope();
      }
   }
}
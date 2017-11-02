using System.Diagnostics;
using Autofac;
using AutofacSamples.WithModules.Services;

namespace AutofacSamples.WithModules.Modules {

   /// <summary>
   /// Module with a dependency, cannot be instantiated automatically by Autofac
   /// We will provide the required dependency, then register one instance of the module
   /// with Autofac
   /// </summary>
   public class WithParameter : Module {

      public string Value { get; }

      public WithParameter(string value) {

#if DEBUG
         Debugger.Break();
#endif
         Value = value;
      }

      protected override void Load(ContainerBuilder builder) {

#if DEBUG
         Debugger.Break();
#endif
         builder
            .RegisterType<Reader>()
            .AsImplementedInterfaces();
      }
   }
}
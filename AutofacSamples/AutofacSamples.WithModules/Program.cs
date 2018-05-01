using Autofac;
using AutofacSamples.WithModules.Modules;
using AutofacSamples.WithModules.Services;

namespace AutofacSamples.Modules {

   /// <summary>
   /// Registering types using modules
   /// One module has a dependency on a parameter to be passed to the ctor
   /// </summary>
   class Program {

      static void Main(string[] args) {

         var builder = new ContainerBuilder();

         // register types from Simple and Simple2
         // WithParameter is filtered out, as it does not inherit from Base
         builder.RegisterAssemblyModules<Base>(typeof(Program).Assembly);

         // Create an instance of WithParameter and
         // register types from WithConstructor
         builder.RegisterModule(new WithParameter("foo"));

         var container = builder.Build();

         // IParser is registered by Simple
         var parser = container.Resolve<IParser>();
         // IRepository is registered by Simple2
         var repository = container.Resolve<IRepository>();
         // IReader is registered by WithConstructor
         // which we explicitly newed and passed to Autofac
         var reader = container.Resolve<IReader>();

         container.Dispose();
      }
   }
}
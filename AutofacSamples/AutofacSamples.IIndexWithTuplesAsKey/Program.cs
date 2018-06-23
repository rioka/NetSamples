using System;
using System.Reflection;
using Autofac;

namespace AutofacSamples.IIndexWithTuplesAsKey
{
  class Program
  {
    static void Main(string[] args)
    {
      var builder = new ContainerBuilder();

      builder
         .RegisterType<RepositoryFactory>()
         .AsSelf();

      builder
         .RegisterType<Service>()
         .AsSelf();

      // registering IRepository implementatins, using 
      // VersionAttribute to compose a key
      builder
         .RegisterAssemblyTypes(typeof(Program).Assembly)
         .Where(t => t.GetInterface(nameof(IRepository)) != null)
         .Keyed<IRepository>(t => {
           var a = t.GetCustomAttribute<VersionAttribute>();
           return Tuple.Create(a.Version, a.Content ?? string.Empty);
         });

      var container = builder.Build();

      var service = container.Resolve<Service>();
      
      Console.WriteLine("Should use RepositoryA");
      service.Do(1, "A");

      Console.WriteLine("Should use RepositoryB");
      service.Do(1);

      Console.WriteLine("Should use RepositoryC");
      service.Do(2);

      Console.WriteLine("Press <ENTER> to quit...");
      Console.ReadLine();
    }
  }
}
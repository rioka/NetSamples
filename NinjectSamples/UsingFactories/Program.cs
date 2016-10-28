using Ninject;
using Ninject.Extensions.Factory;
using UsingFactories.Core.FactoryDelegate;
using UsingFactories.Core.FunctionFactory;
using UsingFactories.Core.SimpleFactory;
using UsingFactories.Core.ExtendedFactory;

namespace UsingFactories
{
  class Program
  {
    // NOTE .InTransientScope will not be released by Ninject

    static void Main(string[] args)
    {
      SimpleFactorySample();
      FunctionFactorySample();
      ExtendedFactorySample();
      DelegateFactorySample();
    }

    static void SimpleFactorySample()
    {
      var kernel = new StandardKernel();

      kernel.Bind<IMapperFactory>().ToFactory();
      kernel.Bind<IMapper>().To<Mapper>();
      kernel.Bind<Sample>().ToSelf();

      var sample = kernel.Get<Sample>();
      sample.Run(12);

      kernel.Dispose();
    }

    static void FunctionFactorySample()
    {
      var kernel = new StandardKernel();

      kernel.Bind<IPopulator>().To<NameBasedPopulator>();
      kernel.Bind<IConfigRepository>().To<AppConfigRepository>();
      kernel.Bind<Builder>().ToSelf();

      var builder = kernel.Get<Builder>();
      var res = builder.Run("me");

      kernel.Dispose();
    }

    static void ExtendedFactorySample()
    {
      var kernel = new StandardKernel();

      kernel.Bind<IResolverFactory>().ToFactory();
      kernel.Bind<IStrategy>().To<SimpleStrategy>();
      kernel.Bind<IResolver>().To<Resolver>();
      kernel.Bind<OtherSample>().ToSelf();

      var otherSample = kernel.Get<OtherSample>();
      otherSample.Run(5);

      kernel.Dispose();
    }

    static void DelegateFactorySample()
    {
      var kernel = new StandardKernel();

      kernel.Bind<ITranslator>().To<Translator>();
      //kernel.Bind<TranslatorFactory>().ToFactory();
      kernel.Bind<Uploader>().ToSelf();


      // ... this has been commented out as well
      var uploader = kernel.Get<Uploader>();
      uploader.Upload("Sample.txt");

      kernel.Dispose();
    }
  }
}

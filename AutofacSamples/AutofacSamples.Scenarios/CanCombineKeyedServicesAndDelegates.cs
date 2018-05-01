using System;
using Autofac;
using Autofac.Core;
using Autofac.Features.Indexed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// We can create a map of delegates, indexed by a value
   /// Autofac then will provide a map of delegates, which resolve the service
   /// for the provided key, and passing additional parameter
   /// So Autofac is not only able to inject factories, but also a map of factories,
   /// combining delegates with <see cref="IIndex{TKey,TValue}"/>
   /// </summary>
   [TestClass]
   public class CanCombineKeyedServicesAndDelegates {

      #region Data

      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         var builder = new ContainerBuilder();
         builder
            .RegisterModule<Installer>();

         _container = builder.Build();
      }

      [TestCleanup]
      public void AfterEach() {

         if (_container != null) {
            _container.Dispose();
         }
      }

      #endregion

      #region Tests

      [TestMethod]
      public void Can_Resolve_Types_Via_Key() {

         // this is not very interesting, just an interim step to the real test

         // act
         var builderA = _container.ResolveKeyed<IBuilder>("A", new Parameter[] { new TypedParameter(typeof(string), "using A")});
         var builderB = _container.ResolveKeyed<IBuilder>("B", new Parameter[] { new TypedParameter(typeof(string), "using B") });

         // assert
         Assert.IsInstanceOfType(builderA, typeof(ABuilder));
         Assert.IsInstanceOfType(builderB, typeof(BBuilder));
      }

      [TestMethod]
      public void Can_Resolve_Types_Using_Indexed_Delegates() {

         // arrange
         var factory = _container.Resolve<IIndex<string, Func<string, IBuilder>>>();

         // act
         var builderA = factory["A"]("Using A");
         var builderB = factory["B"]("Using B");

         // assert
         Assert.IsInstanceOfType(builderA, typeof(ABuilder));
         Assert.IsInstanceOfType(builderB, typeof(BBuilder));
      }

      [TestMethod]
      public void Can_Resolve_Types_Using_Injected_Delegates() {

         // arrange
         var service = _container.Resolve<ConsumingService>();

         // act
         var builderA = service.Run("A", "Using A");
         var builderB = service.Run("B", "Using B");

         // assert
         Assert.AreEqual(typeof(ABuilder).Name, builderA);
         Assert.AreEqual(typeof(BBuilder).Name, builderB);
      }

      #endregion

      #region Internals

      class Installer : Module {

         protected override void Load(ContainerBuilder builder) {

            builder
               .RegisterType<ABuilder>()
               .Keyed<IBuilder>("A");
            builder
               .RegisterType<BBuilder>()
               .Keyed<IBuilder>("B");
            builder
               .RegisterType<Parser>()
               .AsSelf();
            builder
               .RegisterType<ConsumingService>()
               .AsSelf();
         }
      }

      interface IBuilder { }

      class ABuilder : IBuilder {

         private readonly Parser _parser;

         public ABuilder(string value, Parser parser) {
            _parser = parser;
         }
      }

      class BBuilder : IBuilder {

         public BBuilder(string value) {
         }
      }

      class Parser {

         public Parser() {

         }
      }

      class ConsumingService {

         private readonly IIndex<string, Func<string, IBuilder>> _factory;

         #region Constructor

         public ConsumingService(IIndex<string, Func<string, IBuilder>> factory) {
            _factory = factory;
         }

         #endregion

         public string Run(string key, string value) {

            var instance = _factory[key](value);

            return instance.GetType().Name;
         }
      }

      #endregion
   }
}
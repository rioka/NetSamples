using Autofac;
using Autofac.Builder;
using Autofac.Features.AttributeFilters;
using AutofacSamples.Scenarios.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// Similar to <see cref="CanRegisterMultipleInstancesByKeys"/>, but explicitly
   /// calling <see cref="IRegistrationBuilder{TLimit,TActivatorData,TRegistrationStyle}.SingleInstance"/>
   /// </summary>
   [TestClass]
   public class CanRegisterSingletonsByKeys {

      #region Data

      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         var builder = new ContainerBuilder();
         builder
            .RegisterType<Repository>()
            .Named<IRepository>("A")
            .SingleInstance();
         builder
            .RegisterType<Repository>()
            .Named<IRepository>("B")
            .SingleInstance();
         builder
            .RegisterType<A>()
            .WithAttributeFiltering()
            .AsSelf();
         builder
            .RegisterType<B>()
            .WithAttributeFiltering()
            .AsSelf();
         builder
            .RegisterType<C>()
            .WithAttributeFiltering()
            .AsSelf();

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
      public void Two_Different_Instances_Are_Injected_Into_Different_Types() {

         // arrange

         // act
         var instanceA = _container.Resolve<A>();
         var instanceB = _container.Resolve<B>();
         var instanceC = _container.Resolve<C>();

         // assert
         Assert.AreEqual(instanceA.Repository, instanceC.Repository);
         Assert.AreNotEqual(instanceA.Repository, instanceB.Repository);
      }

      [TestMethod]
      public void Singleton_Scope_Is_Obeyed() {

         // arrange

         // act
         var instanceA1 = _container.Resolve<A>();
         var instanceA2 = _container.Resolve<A>();

         // assert
         Assert.AreEqual(instanceA1.Repository, instanceA2.Repository);
      }

      #endregion

      #region Internals

      /// <summary>
      /// Depends on repository registered as "A"
      /// </summary>
      class A {

         public IRepository Repository { get; private set; }

         public A([KeyFilter("A")] IRepository repository) {

            Repository = repository;
         }
      }

      /// <summary>
      /// Depends on repository registered as "B"
      /// </summary>
      class B {

         public IRepository Repository { get; private set; }

         public B([KeyFilter("B")] IRepository repository) {

            Repository = repository;
         }
      }

      /// <summary>
      /// Depends on repository registered as "A"
      /// </summary>
      class C {

         public IRepository Repository { get; private set; }

         public C([KeyFilter("A")] IRepository repository) {

            Repository = repository;
         }
      }


      #endregion
   }
}
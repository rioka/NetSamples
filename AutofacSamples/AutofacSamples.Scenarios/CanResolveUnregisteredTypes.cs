using Autofac;
using Autofac.Core.Registration;
using Autofac.Features.ResolveAnything;
using AutofacSamples.Scenarios.Core.Models;
using AutofacSamples.Scenarios.Core.Repositories;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// Autofac can resolve a service even when it has not been explicitly registered
   /// into the container, as long as we add <see cref="AnyConcreteTypeNotAlreadyRegisteredSource"/>
   /// as a registration source.
   /// There are indeed some limitations
   /// - will be resolved as transient instances
   /// - will be resolved only via its own type
   /// </summary>
   [TestClass]
   public class CanResolveUnregisteredTypes {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();
         // each type is registered as its own service
         // works for open generics too
         _builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
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
      public void Can_Resolve_A_Type_Requesting_It() {

         // arrange
         _container = _builder.Build();

         // act
         _container.Resolve<Repository>();
         
         // assert
      }

      [TestMethod]
      [ExpectedException(typeof(ComponentNotRegisteredException))]
      public void Cannot_Resolve_A_Type_Requesting_Its_Interface() {

         // arrange
         _container = _builder.Build();

         // act
         _container.Resolve<IRepository>();

         // assert
      }

      [TestMethod]
      public void Can_Resolve_An_Open_Generic_Type_Requesting_Its_Closed_Implementation() {

         // arrange
         _container = _builder.Build();

         // act
         _container.Resolve<Parser<Customer>>();
         
         // assert
      }

      [TestMethod]
      [ExpectedException(typeof(ComponentNotRegisteredException))]
      public void Cannot_Resolve_An_Open_Generic_Type_Requesting_An_Interface() {

         // arrange
         _container = _builder.Build();

         // act
         _container.Resolve<IParser<Customer>>();

         // assert
      }

      #endregion
   }
}

using Autofac;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class CanResolveViaAllRegisteredServices {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();
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
      public void Can_Resolve_Via_Base_Type() {

         // arrange
         _builder
            .RegisterType<CustomerService>()
            .As<ServiceBase>();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<ServiceBase>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(CustomerService));
      }

      [TestMethod]
      public void Can_Resolve_Via_Interfaces() {

         // arrange
         _builder
            .RegisterType<CustomerService>()
            .AsImplementedInterfaces()
            .As<ServiceBase>();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IUIService>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(CustomerService));
      }

      [TestMethod]
      public void Can_Resolve_Via_Base_Service() {

         // arrange
         _builder
            .RegisterType<CustomerService>()
            .AsImplementedInterfaces()
            .As<ServiceBase>();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IServiceBase>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(CustomerService));
      }

      #endregion
   }
}

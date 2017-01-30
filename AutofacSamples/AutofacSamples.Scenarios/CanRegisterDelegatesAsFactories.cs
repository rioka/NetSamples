using System.Reflection;
using Autofac;
using Autofac.Builder;
using AutofacSamples.Scenarios.Core.Factories;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class CanRegisterDelegatesAsFactories {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();
         _builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces();
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
      public void Can_Resolve_Types_Via_Factories() {

         // arrange (not necessary though, factory are automatically provided since Autofac 2)
         _builder.RegisterGeneratedFactory<FactoryDelegates.ServiceFactory>();
         _container = _builder.Build();

         // act
         var factory = _container.Resolve<FactoryDelegates.ServiceFactory>();
         var instance = factory();

         // assert
         Assert.IsInstanceOfType(instance, typeof(IService));
      }

      [TestMethod]
      public void Values_Passed_To_Factories_Goes_Into_The_Component_Ctor() {

         // arrange (not necessary though, factory are automatically provided since Autofac 2)
         _builder.RegisterGeneratedFactory<FactoryDelegates.ExtendedServiceFactory>();
         _container = _builder.Build();

         // act
         var factory = _container.Resolve<FactoryDelegates.ExtendedServiceFactory>();
         var instance = factory(2);

         // assert
         Assert.IsInstanceOfType(instance, typeof(IExtendedService));
         Assert.AreEqual(2, instance.Retries);
      }
      
      #endregion
   }
}

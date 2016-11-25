using System;
using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using AutofacSamples.Scenarios.Core.Models;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {
   [TestClass]
   public class OpenGenericNeedsSpecialCare {

      private ContainerBuilder _builder;
      private IContainer _container;


      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();
         // register "standard" types only
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
      [ExpectedException(typeof(ComponentNotRegisteredException))]
      public void Open_Generic_Types_Must_Be_Registered_Explicitly() {

         // arrange
         _container = _builder.Build();

         // act
         _container.Resolve<Parser<Customer>>();
         // assert
      }

      [TestMethod]
      public void Open_Generic_Are_Resolved_when_Registered_Properly() {

         // arrange
         _builder
            .RegisterGeneric(typeof(Parser<>))
            .AsImplementedInterfaces();
         
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IParser<Customer>>();
         
         // assert
         Assert.IsInstanceOfType(instance, typeof(IParser<Customer>));
      }

      #endregion
   }
}

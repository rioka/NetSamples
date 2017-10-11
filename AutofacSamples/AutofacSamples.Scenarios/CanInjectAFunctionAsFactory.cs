using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Autofac;
using Autofac.Core;
using AutofacSamples.Scenarios.Core.FunctionsAsFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// Autofac can generate functions (Dynamic instantiation) and function with parameters (Parametrized instantiation)
   /// so we can set a Func<> as a dependency, and it will be resolved as far as the function has no duplicated parameter types
   /// ie <code>Func&lt;int, SomeType&gt;<code> is valid, but <code>Func&lt;int, int, SomeType&gt;<code> is not, as
   /// it has two <code>int</code> parameters
   /// </summary>
   [TestClass]
   public class CanInjectAFunctionAsFactory {

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
      public void Can_Resolve_Function_Factories() {

         // arrange
         _container = _builder.Build();

         // act
         var factory = _container.Resolve<Func<string, ILineProcessor>>();
         var instance = factory("A");

         // assert
         Assert.IsInstanceOfType(instance, typeof(ILineProcessor));
      }

      [TestMethod]
      public void Can_Resolve_Types_Via_Functions() {

         // arrange
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IFileReader>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(IFileReader));
      }

      [TestMethod]
      public void Cannot_Resolve_Function_Factories_With_Duplicated_Types() {

         // arrange
         _container = _builder.Build();
         ILineProcessor instance = null;

         // act
         var factory = _container.Resolve<Func<string, string, ILineProcessor>>();

         try {
            instance = factory("A", "B");
         }
         catch (DependencyResolutionException dre) {
            Debug.WriteLine(dre.Message);
         }

         // assert
         Assert.IsInstanceOfType(factory, typeof(Func<string, string, ILineProcessor>));
         Assert.IsNull(instance);
      }

      [TestMethod]
      public void Can_Resolve_Function_Factories_With_Multiple_Non_Duplicated_Types() {

         // arrange
         _container = _builder.Build();

         // act
         var factory = _container.Resolve<Func<string, int, ILineProcessor>>();
         var instance = factory("A", 1);

         // assert
         Assert.IsInstanceOfType(factory, typeof(Func<string, int, ILineProcessor>));
         Assert.IsInstanceOfType(instance, typeof(ILineProcessor));
      }

      [TestMethod]
      public void Can_Resolve_Function_Factories_With_Multiple_Closed_Generic_Types() {

         // arrange
         _container = _builder.Build();

         // act
         var factory = _container.Resolve<Func<string, IDictionary<int, string>, IDictionary<int, int>, ILineProcessor>>();
         var instance = factory("A", new Dictionary<int, string>() { }, new Dictionary<int, int>() { });

         // assert
         Assert.IsInstanceOfType(factory, typeof(Func<string, IDictionary<int, string>, IDictionary<int, int>, ILineProcessor>));
         Assert.IsInstanceOfType(instance, typeof(ILineProcessor));
      }

      #endregion
   }
}
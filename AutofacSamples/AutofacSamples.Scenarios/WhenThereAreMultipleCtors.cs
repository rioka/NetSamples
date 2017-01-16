using Autofac;
using Autofac.Core;
using AutofacSamples.Scenarios.Core.IndexedTypes;
using AutofacSamples.Scenarios.Core.Repositories;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// When a type has has multiple, Autofac must be able to properly select one and only one.
   /// If there is ambiguity (eg we cannot choose among ctors with the same # of parameters),
   /// an exception will be raised
   /// </summary>
   [TestClass]
   public class WhenThereAreMultipleCtors {

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
      [ExpectedException(typeof(DependencyResolutionException))]
      [Description("We get an exception with ctors have the same # of parameters")]
      public void When_All_Dependencies_Can_Be_Resolved_But_Ctors_Have_The_Same_No_Of_Parameters() {

         // arrange
         // since both ctors have the same # of parameters, Autofac cannot prefer one over the other
         // and an exception is raised
         _builder.RegisterType<Repository>().As<IRepository>();
         _builder.RegisterType<ProductMapper>().AsSelf();
         _builder.RegisterType<ServiceWithMultipleCtors>().As<IServiceWithMultipleCtors>();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IServiceWithMultipleCtors>();
      }

      [TestMethod]
      [Description("Will pick the ctor for which we can resolve types")]
      public void Ctor_With_Resolvable_Dependencies_Will_Be_Chosen() {

         // arrange
         // we do not provide all dependencies to Autofac, so that only one of the available ctors
         // will be actually available
         // and an exception is raised
         _builder.RegisterType<Repository>().As<IRepository>();
         _builder.RegisterType<ServiceWithMultipleCtors>().As<IServiceWithMultipleCtors>();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IServiceWithMultipleCtors>();

         // assert
         Assert.IsNotNull(instance.Repository);
         Assert.IsNull(instance.Mapper);
      }

      #endregion
   }
}
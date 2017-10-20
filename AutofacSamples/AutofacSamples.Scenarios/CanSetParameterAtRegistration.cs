using System.Reflection;
using Autofac;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// We can explicitly set values for parameters to the ctor
   /// when registering components
   /// We can set parameter values
   /// - via name
   /// - via type (if no two parameters have the same type)
   /// </summary>
   [TestClass]
   public class CanSetParameterAtRegistration {

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
            .Except<ExtendedService>()
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
      public void Can_Register_A_Type_Setting_Values_For_Parameters_By_Type() {

         // arrange
         _builder
            .RegisterType<ExtendedService>()
            .WithParameter(new TypedParameter(typeof(int), 2))
            .AsImplementedInterfaces();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IExtendedService>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(IExtendedService));
         Assert.AreEqual(2, instance.Retries);
      }

      [TestMethod]
      public void Can_Register_A_Type_Setting_Values_For_Parameters_By_Name() {

         // arrange
         _builder
            .RegisterType<ExtendedService>()
            .WithParameter("retries", 2)
            .AsImplementedInterfaces();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IExtendedService>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(IExtendedService));
         Assert.AreEqual(2, instance.Retries);
      }

      #endregion
   }
}

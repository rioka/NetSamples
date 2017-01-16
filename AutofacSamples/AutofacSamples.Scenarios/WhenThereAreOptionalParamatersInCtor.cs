using System.Data.Common;
using Autofac;
using AutofacSamples.Scenarios.Core.Repositories;
using AutofacSamples.Scenarios.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class WhenThereAreOptionalParamatersInCtor {

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
      public void Can_Set_The_Desired_One() {

      }

      [TestMethod]
      [Description("The ctor will be called with 'null' for an optional ctor parameter")]
      public void Will_Call_The_Ctor_With_Null_For_The_Unregistered_Optional_Type() {

         // arrange
         // register Service and tis dependencies
         _builder.RegisterType<Service>().As<IService>();
         _builder.RegisterType<Repository>().As<IRepository>();
         // do not registered DbConnection into the container 
         _builder.RegisterType<ServiceWithOptionalParamsInCtor>()
            .As<IServiceWithOptionalParamsInCtor>();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IServiceWithOptionalParamsInCtor>();

         // assert
         Assert.IsNull(instance.Connection);
      }

      [TestMethod]
      [Description("We can force 'null' for an optional ctor parameter")]
      public void We_Can_Force_Null_For_An_Optional_Ctor_Parameter() {

         // arrange
         // register Service and tis dependencies
         _builder.RegisterType<Service>().As<IService>();
         _builder.RegisterType<Repository>().As<IRepository>();
         _builder.RegisterType<DbConnection>().AsSelf();
         // we force the parameter to be null
         _builder.RegisterType<ServiceWithOptionalParamsInCtor>()
            .As<IServiceWithOptionalParamsInCtor>()
            .WithParameter(TypedParameter.From<DbConnection>(null));
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IServiceWithOptionalParamsInCtor>();

         // assert
         Assert.IsNull(instance.Connection);
      }

      #endregion
   }
}
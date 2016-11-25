using System.Linq;
using System.Reflection;
using Autofac;
using AutofacSamples.Scenarios.Core.ClosedGenerics;
using AutofacSamples.Scenarios.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class CloseGenericNeedNoSpecialCare {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();
         // Does not work
         //_builder
         //   .RegisterType(typeof(IRepository<>))
         //   .AsImplementedInterfaces();
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
      public void Can_Resolve_Closed_Generic_Types_Explicitly_Registered() {

         // arrange
         _builder
            .RegisterType<CustomerRepository>()
            .AsImplementedInterfaces();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IRepository<Customer>>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(CustomerRepository));
      }

      [TestMethod]
      public void Can_Resolve_Closed_Generic_Types_Registered_By_Convention() {

         // arrange
         _builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IRepository<Customer>>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(CustomerRepository));
      }

      [TestMethod]
      public void Can_Resolve_Closed_Generic_Types_Registered_By_Explicitly_Selecting_Closed_Types() {

         // arrange
         _builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => !t.IsAbstract
                        && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)))
            .AsImplementedInterfaces();
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IRepository<Customer>>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(CustomerRepository));
      }

      #endregion
   }
}

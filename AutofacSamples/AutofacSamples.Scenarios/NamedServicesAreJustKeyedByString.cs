using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutofacSamples.Scenarios.Core.IndexedTypes;
using AutofacSamples.Scenarios.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class NamedServicesAreJustKeyedByString {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();

         // get all types implementing BaseMapper (IMapper is just a marker interface)
         var types = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && t.GetInterfaces().Any(i => i == typeof(IMapper)));
         // and registering them indexed on the target (ie mapped) type (the T in BaseMapper<T>)
         foreach (var type in types) {
            // this can be improved, but for now it's enough
            var typeKey = type.BaseType.GenericTypeArguments.First().Name;
            _builder.RegisterType(type).Named<IMapper>(typeKey);
         }
         // register the first one again, with another, different key
         // so as to have it registered twice, with different keys
         // and verifying that we get get that implementation using both
         var t0 = types.First();
         _builder.RegisterType(t0).Named<IMapper>(t0.Name + "0");

         // then register the factory which will return the proper mapper for the type to map
         _builder.RegisterType<MapperFactory>().AsSelf();
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
      public void Can_Resolve_A_Type_At_Runtime() {

         // arrange
         _container = _builder.Build();
         var factory = _container.Resolve<MapperFactory>();

         // act
         var mapper = factory.GetMapper<Customer>();

         // assert
         Assert.IsInstanceOfType(mapper, typeof(CustomerMapper));
      }

      #endregion
   }
}

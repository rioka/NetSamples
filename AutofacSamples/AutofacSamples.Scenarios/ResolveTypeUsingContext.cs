using System.Linq;
using System.Reflection;
using Autofac;
using AutofacSamples.Scenarios.Core.IndexedTypes;
using AutofacSamples.Scenarios.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class ResolveTypeUsingContext {

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
            _builder.RegisterType(type).Keyed<IMapper>(typeKey);
         }
         // register the first one again, with another, different key
         // so as to have it registered twice, with different keys
         // and verifying that we get get that implementation using both
         var t0 = types.First();
         _builder.RegisterType(t0).Keyed<IMapper>(t0.Name + "0");

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

      [TestMethod]
      public void Can_Resolve_A_Type_By_Name() {

         // arrange
         _container = _builder.Build();
         var factory = _container.Resolve<MapperFactory>();

         // act
         var mapper = factory.GetMapper(typeof(Product).Name);

         // assert
         Assert.IsInstanceOfType(mapper, typeof(ProductMapper));
      }

      [TestMethod]
      public void Can_Resolve_The_Same_Type_With_More_Than_One_Key() {

         // arrange
         _container = _builder.Build();
         var factory = _container.Resolve<MapperFactory>();

         // act
         var mapper1 = factory.GetMapper<Customer>();
         var mapper2 = factory.GetMapper(typeof(CustomerMapper).Name + "0");

         // assert
         Assert.IsInstanceOfType(mapper1, typeof(CustomerMapper));
         Assert.IsInstanceOfType(mapper2, typeof(CustomerMapper));
      }


      [TestMethod]
      public void Dynamic_To_The_Rescue() {

         // arrange
         _container = _builder.Build();
         var factory = _container.Resolve<MapperFactory>();

         // we do not know the type of the mapper, so we cannot cast it and call its method
         // without reflection
         // we cannot even cast to a generic interface, because, again, it would be a
         // generic interface and we do not know the type at compile time
         // Declaring it as "dynamic" allow us to call mapper's methods without compiler's errors
         // even without knowing its type
         // act
         dynamic mapper = factory.GetMapper(typeof(Product).Name);

         // assert
         var mapped = mapper.Map("");
         Assert.IsInstanceOfType(mapped, typeof(Product));
      }

      #endregion
   }
}

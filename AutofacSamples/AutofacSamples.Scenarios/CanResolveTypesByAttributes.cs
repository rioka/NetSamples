using Autofac;
using Autofac.Features.AttributeFilters;
using AutofacSamples.Scenarios.Core.ResolvingWithAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// We have multiple implementations for the same interface:
   /// we want to inject a specific implementation, adding a parameter
   /// to the injected type (<see cref="KeyFilter"/>), and registering
   /// each implementation with its own given key
   /// This way Autofac will inject the desired implementation for 
   /// the service
   /// </summary>
   [TestClass]
   public class CanResolveTypesByAttributes {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _builder = new ContainerBuilder();
         // register SimpleMapper as IMapper implementation when injecting as IMapper
         // marked with KeyFilter("simple") attribute
         _builder
            .RegisterType<SimpleMapper>().Keyed<IMapper>("simple");
         // register FullMapper as IMapper implementation when injecting as IMapper
         // marked with KeyFilter("complex") attribute
         _builder
            .RegisterType<FullMapper>().Keyed<IMapper>("complex");
         // Instruct Autofac to use attribute-based filter
         _builder
            .RegisterType<Processor>().WithAttributeFiltering().AsSelf();
         _builder
            .RegisterType<Importer>().WithAttributeFiltering().AsSelf();
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
      public void Can_Resolve_Types_Using_KeyFilter_In_Ctor() {

         // arrange
         _container = _builder.Build();

         // act
         var processor = _container.Resolve<Processor>();
         var importer = _container.Resolve<Importer>();

         // assert
         Assert.IsInstanceOfType(processor.Mapper, typeof(FullMapper));
         Assert.IsInstanceOfType(importer.Mapper, typeof(SimpleMapper));
      }

      #endregion
   }
}
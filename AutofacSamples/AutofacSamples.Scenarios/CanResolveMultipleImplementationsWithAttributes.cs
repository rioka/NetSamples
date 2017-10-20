using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Features.AttributeFilters;
using AutofacSamples.Scenarios.Core.ResolvingWithMetadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   [TestClass]
   public class CanResolveMultipleImplementationsWithAttributes {

      #region Vars

      private ContainerBuilder _builder;
      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         var assembly = Assembly.GetExecutingAssembly();

         _builder = new ContainerBuilder();

         _builder.RegisterAssemblyTypes(assembly)
            .InNamespaceOf<DdsProcessor>()
            .Where(t => t.Name.EndsWith("Processor"))
            .WithAttributeFiltering()        // enable attribute-based filtering
            .AsSelf();

         var types = assembly
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i == typeof(IFailureResolver)));
         
         foreach (var type in types) {
            var fsAttr = type.GetCustomAttribute<FinanceSystemAttribute>();
            
            var rb = _builder.RegisterType(type);
            foreach (var system in fsAttr.Systems) {
               // set all target finance system for the service
               rb.Keyed<IFailureResolver>(system);
            }
         }
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
         var ddsProcessor = _container.Resolve<DdsProcessor>();
         var sbmsProcessor = _container.Resolve<SbmsProcessor>();

         // assert
         Assert.IsInstanceOfType(ddsProcessor.Resolver, typeof(DdsFailureResolver));
         Assert.IsInstanceOfType(sbmsProcessor.Resolver, typeof(DefaultFailureResolver));
      }

      #endregion
   }
}
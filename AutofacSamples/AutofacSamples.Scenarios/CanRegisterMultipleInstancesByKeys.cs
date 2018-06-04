using Autofac;
using AutofacSamples.Scenarios.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// We register specific instances, each with a different key
   /// We then request each by its key, and get the same instance
   /// for that key
   /// A sort of "multiple" singletons!
   /// If the same type is registered without a key as well,
   /// the last named instance is returned when asking for an
   /// instance without a key (as the last registration wins in Autofac)
   /// </summary>
   [TestClass]
   public class CanRegisterMultipleInstancesByKeys {

      #region Data

      private Repository _repoByKey1;
      private Repository _repoByKey2;
      private ILifetimeScope _scope;

      private IContainer _container;

      #endregion

      #region Setup & teardown

      [TestInitialize]
      public void BeforeEach() {

         _repoByKey1 = new Repository();
         _repoByKey2 = new Repository();

         var builder = new ContainerBuilder();
         builder
            .RegisterInstance(_repoByKey1)
            .Named<Repository>("1");
         builder
            .RegisterInstance(_repoByKey2)
            .Named<Repository>("2");
         builder
            .RegisterInstance(_repoByKey2);

         _container = builder.Build();
      }

      [TestCleanup]
      public void AfterEach() {

         if (_scope != null) {
            _scope.Dispose();
         }

         if (_container != null) {
            _container.Dispose();
         }
      }

      #endregion

      #region Tests

      [TestMethod]
      public void Different_Instances_Are_Returned_For_Different_Keys() {

         // act
         var instance1 = _container.ResolveNamed<Repository>("1");
         var instance2 = _container.ResolveNamed<Repository>("2");

         // assert
         Assert.AreSame(_repoByKey1, instance1);
         Assert.AreSame(_repoByKey2, instance2);
      }

      [TestMethod]
      public void Same_instance_Is_Returned_For_Same_Key() {

         // act
         var instance1 = _container.ResolveNamed<Repository>("1");
         var instance2 = _container.ResolveNamed<Repository>("1");

         // assert
         Assert.AreSame(_repoByKey1, instance1);
         Assert.AreSame(instance1, instance2);
      }

      [TestMethod]
      public void Scope_Are_Obeyed() {

         // arrange
         _scope = _container.BeginLifetimeScope();
         
         // act
         var instance1 = _container.ResolveNamed<Repository>("1");
         var instance2 = _scope.ResolveNamed<Repository>("1");

         // assert
         Assert.AreSame(_repoByKey1, instance1);
         Assert.AreSame(instance1, instance2);
      }

      [TestMethod]
      public void Last_Registration_Wins_For_An_Unnamed_Resolve_Request() {

         // act
         var instance1 = _container.ResolveNamed<Repository>("1");
         var instance2 = _container.Resolve<Repository>();

         // assert
         Assert.AreSame(_repoByKey1, instance1);
         Assert.AreSame(_repoByKey2, instance2);
      }

      #endregion
   }
}
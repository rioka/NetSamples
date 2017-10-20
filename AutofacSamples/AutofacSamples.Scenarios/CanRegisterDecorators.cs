using Autofac;
using AutofacSamples.Scenarios.Core.Handlers;
using AutofacSamples.Scenarios.Core.Handlers.Decorators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios {

   /// <summary>
   /// We can define decorators for a component, implementing a service, then registering the component and its decorators
   /// Then we can request for an instance of the service, and have the decorated implementation of the component
   /// </summary>
   [TestClass]
   public class CanRegisterDecorators {

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
      public void Can_Resolve_Decorators() {

         // arrange
         // register Handler as IHandler, assigning the name "handler" to this component...
         _builder
            .RegisterType<Handler>()
            .Named<IHandler>("handler");

         // ... and then get LoggingHandlerDecorator applied when resolving an IHandler, using the registration named "handler" for the inner component, 
         // and assigning the name "logginghandler" to this component...
         _builder
            .RegisterDecorator<IHandler>((ctx, inner) => new LoggingHandlerDecorator(inner), "handler")
            .Named<IHandler>("logginghandler");
         
         // ... and then get TransactionalHandlerDecorator applied when resolving an IHandler, using the registration named "loggingandler" for the inner component
         // no name assiogned to this one, since it will not be further decorated 
         _builder
            .RegisterDecorator<IHandler>((ctx, inner) => new TransactionalHandlerDecorator(inner), "logginghandler");
         _container = _builder.Build();

         // act
         var instance = _container.Resolve<IHandler>();

         // assert
         Assert.IsInstanceOfType(instance, typeof(TransactionalHandlerDecorator));
      }

      #endregion
   }
}

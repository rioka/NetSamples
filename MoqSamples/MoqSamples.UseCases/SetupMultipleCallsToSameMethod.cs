using Moq;
using MoqSamples.UseCases.Core.Models;
using MoqSamples.UseCases.Core.Services;
using NUnit.Framework;

namespace MoqSamples.UseCases {

   [TestFixture]
   public class SetupMultipleCallsToSameMethod {

      #region Vars

      private Mock<IClient> _client;
      private CustomerService _sut;

      #endregion

      #region Setup & teardown

      [SetUp]
      public void BeforeEach() {

         _client = new Mock<IClient>();
         _sut = new CustomerService(_client.Object);
      }

      #endregion

      [Test]
      public void Can_Setup_A_Sequence_Of_Calls_With_Different_Results() {

         // arrange
         var customers = new[] {
            new Customer(),
            new Customer(),
            new Customer()
         };
         _client
            .SetupSequence(c => c.Connect())
            .Returns(true)
            .Returns(false)
            .Returns(true);

         // act
         var failed = _sut.Update(customers);

         // assert
         Assert.AreEqual(1, failed);
      }
   }
}

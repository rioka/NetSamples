using AutoFixtureSamples.Core;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Moq;
using AutoFixtureSamples.Core.Models;

namespace AutoFixtureSamples.Tests {

   [TestFixture]
   public class CustomerServiceTests {

      IFixture _fixture;
      ICustomerService _sut;


      public void BeforeEach() {

         _fixture = new Fixture().Customize(new AutoMoqCustomization());
         _sut = _fixture.Create<ICustomerService>();
      }

      [Test]
      public void Can_Get_Customer() {

         // arrange
         var expectedCustomer = new Customer();
         var repository = _fixture.Freeze<Mock<ICustomerRepository>>();
         repository
            .Setup(r => r.Get(1))
            .Returns(expectedCustomer);

         // act
         var customer = _sut.Get(1);

         // assert
         Assert.IsNotNull(customer);
      }
   }
}

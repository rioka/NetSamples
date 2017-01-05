using Moq;
using MoqSamples.UseCases.Core.Mappers;
using MoqSamples.UseCases.Core.Models;
using NUnit.Framework;

namespace MoqSamples.UseCases {

   [TestFixture]
   public class WorksWithDynamic {

      /// <summary>
      /// Cover the case when, eg, we have a factory for a generic type
      /// not known at compile time, so we return an instance of a generic
      /// marker interface (ie IMapper). Then we save the mocked instance
      /// as <code>dynamic</code> whithin the method being tested in the real applicatiion, 
      /// so that we can call any method without complain by the compiler.
      /// So, to sum up
      /// - we would inject the mock
      /// </summary>
      [Test]
      public void Works_With_Dynamic() {
         
         // arrange
         var mapper = new Mock<BaseMapper<Customer>>();
         var customer = new Customer();
         mapper
            .Setup(m => m.Map(It.IsAny<string>()))
            .Returns(customer);
         dynamic instance = mapper.Object;

         // act
         var result = instance.Map("");

         // assert
         Assert.AreSame(customer, result);
      }

      [Test]
      public void Use_Dynamic_For_Mocked_Instances() {

         // arrange
         var mapper = new Mock<BaseMapper<Customer>>(MockBehavior.Strict);
         var factory = new Mock<IMapperFactory>();
         factory
            .Setup(f => f.Get(It.IsAny<string>()))
            .Returns(mapper.Object);
         var customer = new Customer();
         mapper
            .Setup(m => m.Map(It.IsAny<string>()))
            .Returns(customer);

         var sut = new MultiTypeMapper(factory.Object);

         // act
         sut.MapFrom("abc");

         // assert
      }
   }
}

using System;
using System.Linq.Expressions;
using Moq;
using MoqSamples.UseCases.Core.Services;
using NUnit.Framework;

namespace MoqSamples.UseCases {

   [TestFixture]
   public class SetConstraintOnExpressionParameter {

      [Test]
      public void Constraint_Works_When_Setup_And_Call_Match() {

         // arrange
         var runner = new Mock<IRunner>();
         runner
            .Setup(x => x.Run(It.Is<Expression<Func<int, int>>>(p => CheckAddTwoExp(p))))
            .Returns(5);
         var service = new SampleService(runner.Object);

         // act
         var result = service.Do(10);

         // assert
         Assert.AreEqual(5, result);
      }

      [Test]
      public void Constraint_Works_When_Setup_And_Call_Dont_Match() {

         // arrange
         var runner = new Mock<IRunner>();
         runner
            .Setup(x => x.Run(It.Is<Expression<Func<int, int>>>(p => CheckAddTwoExp(p))))
            .Returns(5);
         var service = new SampleService(runner.Object);

         // act
         var result = service.DoOther(10);

         // assert
         Assert.AreEqual(0, result);
      }

      #region Internals

      static bool CheckAddTwoExp(Expression<Func<int, int>> e) {

         var param = e.Parameters[0];
         var operation = (BinaryExpression) e.Body;
         var left = (ParameterExpression) operation.Left;
         var right = (ConstantExpression) operation.Right;

         return operation.NodeType == ExpressionType.Add
                && (int) right.Value == 2;
      }

      #endregion
   }
}
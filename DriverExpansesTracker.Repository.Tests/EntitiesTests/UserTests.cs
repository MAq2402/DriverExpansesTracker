using DriverExpansesTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace DriverExpansesTracker.Repository.Tests.EntitiesTests
{
    public class UserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConstructorShouldThrowArgumentNullException_1(string firstName)
        {
            Assert.Throws<ArgumentNullException>(() => new User(firstName, "mock"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConstructorShouldThrowArgumentNullException_2(string  lastName)
        {
            Assert.Throws<ArgumentNullException>(() => new User("mock",lastName));
        }

        [Theory]
        [InlineData(10,false,20,true,30,false,50,true,20,10,50,30)]
        [InlineData(10, false, 20, false, 30, false, 50, false, 0, 30, 0, 80)]
        [InlineData(10, true, 20, true, 30, true, 50, true, 30, 0,80, 0)]

        public void UpdatePaymentsTest(decimal amount1,bool isPayed1,
                                       decimal amount2, bool isPayed2, 
                                       decimal amount3, bool isPayed3, 
                                       decimal amount4, bool isPayed4,
                                       decimal received,decimal toReceive, decimal payed, decimal toPay)
        {
            //Arrange
            var user = new User("mock", "mock");
            var payment1 = new Payment("mock", "mock", 1, amount1);

            if (isPayed1)
            {
                payment1.AcceptPayment();
            }
            var payment2 = new Payment("mock", "mock", 1, amount2);
            if (isPayed2)
            {
                payment2.AcceptPayment();
            }
            var payment3 = new Payment("mock", "mock", 1, amount3);
            if (isPayed3)
            {
                payment3.AcceptPayment();
            }
            var payment4 = new Payment("mock", "mock", 1, amount4);
            if (isPayed4)
            {
                payment4.AcceptPayment();
            }

            var receivedPayments = new List<Payment>()
            {
                payment1,
                payment2
            };
            var payedPayments = new List<Payment>()
            {
                payment3,
                payment4
            };
            user.ReceivedPayments.AddRange(receivedPayments);
            user.PayedPayments.AddRange(payedPayments);


            //Act
            user.UpdatePayments();
            var expectedPayed = payed;
            var actualPayed = user.Payed;

            var expectedToPay = toPay;
            var actualToPay = user.ToPay;

            var expectedReceived = received;
            var actualReceived = user.Received;

            var expectedToReceive = toReceive;
            var actualToReceive = user.ToReceive;

            //Assert
            Assert.Equal(expectedPayed, actualPayed);
            Assert.Equal(expectedToPay, actualToPay);
            Assert.Equal(expectedReceived, actualReceived);
            Assert.Equal(expectedToReceive, actualToReceive);
        }
    }
}

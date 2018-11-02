using DriverExpansesTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Repository.Tests.EntitiesTests
{
    public class PaymentTests
    {
        [Theory]
        [InlineData(-5)]
        [InlineData(-0.01)]
        [InlineData(int.MinValue)]
        public void ConstructorShouldThrowArgumentException(decimal amount)
        {
            Assert.Throws<ArgumentException>(() => new Payment("mock", "mock", 1, amount));
        }

        [Fact]
        public void AcceptPaymentShouldWork()
        {
            var payment = new Payment("mock", "mock", 1, 1);

            payment.AcceptPayment();

            var expected = true;
            var actual = payment.IsPayed;

            Assert.Equal(expected, actual);
        }
        
        [Fact]

        public void AcceptPaymentShouldThrowInvalidOperationException()
        {
            var payment = new Payment("mock", "mock", 1, 1);

            payment.AcceptPayment();

            Assert.Throws<InvalidOperationException>(()=>payment.AcceptPayment());
        }

    }
}

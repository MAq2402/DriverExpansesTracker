using DriverExpansesTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Repository.Tests.EntitiesTests
{
    public class PassengerRouteTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConstructorTestShouldThrowArgumentNullException(string start)
        {
            Assert.Throws<ArgumentNullException>(() => new PassengerRoute(start, 1, "mock", 1));
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void ConstructorTestShouldThrowArgumentException(int length)
        {
            Assert.Throws<ArgumentException>(() => new PassengerRoute("mock", length, "mock", 1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SetDestinationTestShouldThrowArgumentNullException(string destiantion)
        {
            var route = new PassengerRoute("mock", 1, "mock", 1);

            Assert.Throws<ArgumentNullException>(() => route.SetDestination(destiantion));
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(int.MinValue)]
        public void SetTotalPriceTestShouldThrowArgumentNullException(decimal totalPrice)
        {
            var route = new PassengerRoute("mock", 1, "mock", 1);

            Assert.Throws<ArgumentException>(() => route.SetTotalPrice(totalPrice));
        }

    }
}

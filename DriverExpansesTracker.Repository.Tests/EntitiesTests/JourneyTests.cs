using DriverExpansesTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Repository.Tests.EntitiesTests
{
    public class JourneyTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]

        public void Consturctor_ShouldThrowArgumentException(int length)
        {
            Action action = () => new Journey("mock", "mock", length, 1, "mock");

            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Consturctor_ShouldThrowArgumentNullException_1(string destination)
        {
            Action action = () => new Journey(destination, "mock", 1, 1, "mock");

            Assert.Throws<ArgumentNullException>(action);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Consturctor_ShouldThrowArgumentNullException_2(string start)
        {
            Action action = () => new Journey("mock",start, 1, 1, "mock");

            Assert.Throws<ArgumentNullException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]

        public void SetTotalPrice_ShouldThrowArgumentException_1(double fuelConsumption)
        {
            var journey = new Journey("mock", "mock", 1, 1, "mock");

            Action action = () => journey.SetTotalPrice(fuelConsumption, 1);

            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]

        public void SetTotalPrice_ShouldThrowArgumentException_2(decimal priceForLiter)
        {
            var journey = new Journey("mock", "mock", 1, 1, "mock");

            Action action = () => journey.SetTotalPrice(1,priceForLiter);

            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(1,1,1,0.01)]
        [InlineData(50,150,20,1500)]
        [InlineData(10.1,12.2,3.3,4.07)]
        [InlineData(121.99,5.66,5.21,35.97)]

        public void SetTotalPrice_ShouldWork(double length,double fuelConsumption, decimal priceForLiter,decimal expected)
        {
            //Arrange
            var journey = new Journey("mock", "mock", length, 1, "mock");

            //Act
            journey.SetTotalPrice(fuelConsumption, priceForLiter);
            var actual = journey.TotalPrice;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}

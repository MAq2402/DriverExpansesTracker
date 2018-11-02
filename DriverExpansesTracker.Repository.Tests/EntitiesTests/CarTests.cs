using DriverExpansesTracker.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Repository.Tests.EntitiesTests
{
    
    public class CarTests
    {
        [Fact]
        public void DisactivateTest_ShouldWork()
        {
            //Arrange
            var car = new Car("mock", "mock", 1, Enums.FuelType.benzine);


            //Act
            car.Disactivate();
            var result = car.Active;

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void DisactivateTest_ShouldThrowInvalidOperationException()
        {
            //Arrange
            var car = new Car("mock", "mock", 1, Enums.FuelType.benzine);


            //Act
            car.Disactivate();
            Action action = () => car.Disactivate();

            //Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]

        public void SetFuellConsumption_ShouldThrowArgumentException(double fuelConsumption)
        {
            Action action  = () => new Car("mock", "mock", fuelConsumption, Enums.FuelType.benzine);

            Assert.Throws<ArgumentException>(action);     
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]

        public void SetName_ShouldThrowArgumentNullException(string name)
        {
            Action action = () => new Car("mock", name, 1, Enums.FuelType.benzine);

            Assert.Throws<ArgumentNullException>(action);
        }
    }
}

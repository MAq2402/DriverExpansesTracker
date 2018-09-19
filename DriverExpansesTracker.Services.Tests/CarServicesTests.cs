using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Services;
using DriverExpansesTracker.Services.Models.Car;
using DriverExpansesTracker.API.Infrastructure;
using DriverExpansesTracker.Repository.Enums;
using DriverExpansesTracker.Services.Tests.Fixtures;
namespace DriverExpansesTracker.Services.Tests
{
    public class CarServiceTests : IClassFixture<MapperFixture>
    {
        private Mock<IRepository<Car>> mockedCarRepository;
        public CarServiceTests()
        {
            mockedCarRepository = new Mock<IRepository<Car>>();
        }
        [Fact]
        void AddCar_ShouldThrowException()
        {
            //Arrange          	
            mockedCarRepository.Setup(r => r.Save()).Returns(false);
            var carService = new CarService(mockedCarRepository.Object);
            var car = new CarForCreationDto();
            //Act	
            Action action = () => carService.AddCar(car, "");
            var expectedMessage = "Could not save car";
            //Assert	
            var exception = Assert.Throws<Exception>(action);
            Assert.Equal(expectedMessage, exception.Message);
        }
        [Fact]
        void AddCar_ShouldReturnGoodCar()
        {
            //Arrange	
            mockedCarRepository.Setup(r => r.Save()).Returns(true);
            var car = new CarForCreationDto
            {
                FuelConsumption100km = 10,
                FuelType = FuelType.benzine,
                Name = "Beta"
            };
            var carService = new CarService(mockedCarRepository.Object);
            //Act	
            var expected = new CarDto
            {
                FuelConsumption100km = 10,
                FuelType = "benzine",
                Id = 0,
                Name = "Beta",
                UserId = "1"
            };
            var actual = carService.AddCar(car, "1");
            //Assert	
            Assert.Equal(expected.FuelConsumption100km, actual.FuelConsumption100km);
            Assert.Equal(expected.FuelType, actual.FuelType);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.UserId, actual.UserId);
        }
        [Fact]
        public void CarExists_ShouldReturnTrue()
        {
            //Arrange	
            //mockedCarRepository.Se	
            //Act	
            //Assert	
        }
    }
}
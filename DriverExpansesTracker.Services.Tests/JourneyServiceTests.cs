using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DriverExpansesTracker.Services.Tests
{
    public class JourneyServiceTests
    {
        private Mock<IRepository<Journey>> mockedJourneyRepository;
        private Mock<IRepository<Car>> mockedCarRepository;
        private JourneyService journeyService;

        public JourneyServiceTests()
        {
            mockedJourneyRepository = new Mock<IRepository<Journey>>();

            mockedJourneyRepository.Setup(j => j.Save()).Returns(true);

            mockedCarRepository = new Mock<IRepository<Car>>();

            journeyService = new JourneyService(mockedJourneyRepository.Object, mockedCarRepository.Object);
        }
        [Fact]
        public void GiveTotalPricesTest_JourneyTotalPrice()
        {
            //Arrange


            var journey = new Journey("mock", "mock", 100, 1, "mock");
            var priceForLiter = 2;
            var fuelConsumption100Km = 10;

            //Act
            journeyService.SetTotalPrices(journey, fuelConsumption100Km, priceForLiter);
            var expected = 20;
            var actual = journey.TotalPrice;
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GiveTotalPricesTest_PassengerRouteTotalPrice_1()
        {
            //Arrange
            var journey = new Journey("mock", "mock", 100, 1, "mock");
            var route = new PassengerRoute("mock", "mock", 100, "mock", 1);
            journey.PassengerRoutes.Add(route);

            var priceForLiter = 2;
            var fuelConsumption100km = 10;
            //Act
            journeyService.SetTotalPrices(journey, priceForLiter, fuelConsumption100km);
            var expected = 10;
            var actual = journey.PassengerRoutes[0].TotalPrice;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GiveTotalPricesTest_PassengerRouteTotalPrice_3()
        {
            //Arrange
            var journey = new Journey("mock", "mockk", 100, 1, "mock");

            var route1 = new PassengerRoute("mock", "mock", 100, "mock", 1);
            var route2 = new PassengerRoute("mock", "mock", 100, "mock", 1);
            var route3 = new PassengerRoute("mock", "mock", 100, "mock", 1);

            journey.PassengerRoutes.Add(route1);
            journey.PassengerRoutes.Add(route2);
            journey.PassengerRoutes.Add(route3);


            var priceForLiter = 2;
            var fuelConsumption100km = 10;

            //Act
            journeyService.SetTotalPrices(journey, priceForLiter, fuelConsumption100km);
            var expected = 5;
            var actual1 = journey.PassengerRoutes[0].TotalPrice;
            var actual2 = journey.PassengerRoutes[1].TotalPrice;
            var actual3 = journey.PassengerRoutes[2].TotalPrice;

            //Assert
            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
        }

        [Fact]
        public void GiveTotalPricesTest_PassengerRouteTotalPrice_4()
        {
            //Arrange
            var journey = new Journey("mock", "mockk", 100, 1, "mock");

            var route1 = new PassengerRoute("mock", "mock", 100, "mock", 1);
            var route2 = new PassengerRoute("mock", "mock", 50, "mock", 1);
            var route3 = new PassengerRoute("mock", "mock", 50, "mock", 1);

            journey.PassengerRoutes.Add(route1);
            journey.PassengerRoutes.Add(route2);
            journey.PassengerRoutes.Add(route3);

            var priceForLiter = 2;
            var fuelConsumption100km = 10;
            //Act
            journeyService.SetTotalPrices(journey, priceForLiter, fuelConsumption100km);

            var expected1 = 7.5m;
            var expected2 = 2.5m;
            var expected3 = 2.5m;

            var actual1 = journey.PassengerRoutes[0].TotalPrice;
            var actual2 = journey.PassengerRoutes[1].TotalPrice;
            var actual3 = journey.PassengerRoutes[2].TotalPrice;

            //Assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
        }

        [Fact]
        public void GiveTotalPricesTest_PassengerRouteTotalPrice_5()
        {
            //Arrange
            var journey = new Journey("mock", "mockk", 100, 1, "mock");

            var route1 = new PassengerRoute("mock", "mock", 75, "mock", 1);
            var route2 = new PassengerRoute("mock", "mock", 50, "mock", 1);
            var route3 = new PassengerRoute("mock", "mock", 25, "mock", 1);
            var route4 = new PassengerRoute("mock", "mock", 10, "mock", 1);

            journey.PassengerRoutes.Add(route1);
            journey.PassengerRoutes.Add(route2);
            journey.PassengerRoutes.Add(route3);
            journey.PassengerRoutes.Add(route4);

            var priceForLiter = 2;
            var fuelConsumption100km = 10;
            //Act
            journeyService.SetTotalPrices(journey, priceForLiter, fuelConsumption100km);

            var expected1 = 5.32m;
            var expected2 = 2.82m;
            var expected3 = 1.15m;
            var expected4 = 0.4m;

            var actual1 = Decimal.Round(journey.PassengerRoutes[0].TotalPrice, 2);
            var actual2 = Decimal.Round(journey.PassengerRoutes[1].TotalPrice, 2);
            var actual3 = Decimal.Round(journey.PassengerRoutes[2].TotalPrice, 2);
            var actual4 = Decimal.Round(journey.PassengerRoutes[3].TotalPrice, 2);

            //Assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
        }
    }
}

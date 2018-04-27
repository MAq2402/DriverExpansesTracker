using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Services;
using Moq;
using System;
using Xunit;

namespace DriverExpansesTracker.Services.Tests
{
    public class JourneyServiceTests
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            var mockedJourneyRepository = new Mock<IRepository<Journey>>();

            var mockedCarRepository = new Mock<IRepository<Car>>();

            var journeyService = new JourneyService(mockedJourneyRepository.Object, mockedCarRepository.Object);

            var journey = new Journey
            {
                Length = 100
            };
            var priceForLiter = 2;
            var fuelConsumption100Km = 10;

            //Act
            //journeyService.AddJourney()
            var expected = 20;
            var actual = journey.TotalPrice;
            //Assert
            Assert.Equal(expected, actual);
        }

        //[TestMethod]
        //public void GiveTotalPricesTest_JourneyTotalPrice()
        //{
        //    //Arrange
        //    var journeyService = new JourneyService();
        //    var journey = new Journey
        //    {
        //        Length = 100
        //    };
        //    var priceForLiter = 2;
        //    var fuelConsumption100km = 10;
        //    //Act
        //    journeyService.GiveTotalPrices(journey, priceForLiter, fuelConsumption100km);

        //    var expected = 20;
        //    var actual = journey.TotalPrice;

        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}
    }
}

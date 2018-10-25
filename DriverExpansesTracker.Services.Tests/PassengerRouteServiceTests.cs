using DriverExpansesTracker.Services.Models.PassengerRoute;
using DriverExpansesTracker.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Repository.Entities;
using System.Linq;

namespace DriverExpansesTracker.Services.Tests
{
    public class PassengerRouteServiceTests
    {
        [Theory]
        [InlineData("1","2","3","4",false)]
        [InlineData("michal", "olek", "ola", "dawid",false)]
        [InlineData("Xd", "xd", "xD", "XD",false)]
        [InlineData("1", "2", "3", "1",true)]
        [InlineData("michal", "olek", "ola", "olek",true)]
        [InlineData("Xd", "xd", "xD", "xD",true)]
        public void SameUserForMultipleRoutes_Test(string id1,string id2,string id3 ,string id4,bool expected)
        {
            //Arange
            var mockedPassengerRouteRepository = new Mock<IRepository<PassengerRoute>>();

            var mockedUserRepository = new Mock<IRepository<User>>();

            var passengerRouteService = new PassengerRouteService(
                mockedPassengerRouteRepository.Object
                ,mockedUserRepository.Object);

            var routes = new List<PassengerRouteForCreationDto>
            {
                new PassengerRouteForCreationDto
                {
                    UserId=id1
                },
                 new PassengerRouteForCreationDto
                {
                    UserId=id2
                },
                  new PassengerRouteForCreationDto
                {
                    UserId=id3
                },
                      new PassengerRouteForCreationDto
                {
                    UserId=id4
                }
            };
            //Act
            var actual = passengerRouteService.SameUserForMultipleRoutes(routes);

            //Assert
            Assert.Equal(expected, actual);     
        }


        [Theory]
        [InlineData("1","2","3","4", "1", "2", "3", "4",true)]
        [InlineData("1", "2", "3", "4", "1", "1", "1", "1",true)]
        [InlineData("1", "2", "3", "4", "1", "1", "2", "2",true)]
        [InlineData("1", "2", "3", "3", "1", "2", "3", "4",false)]
        [InlineData("3", "2", "3", "4", "1", "2", "3", "4",false)]
        [InlineData("1", "5", "3", "4", "1", "1", "2", "2",false)]
        public void RoutesUsersExist_Tests(string userId1,string userId2, string userId3, string userId4,
                                     string routeId1, string routeId2, string routeId3, string routeId4,bool expected)
        {
            //Arange
            var mockedUserRepository = new Mock<IRepository<User>>();
            var users = new List<User>
            {
                new User("mock","mock")
                {
                    Id=userId1
                },
                new User("mock","mock")
                {
                    Id=userId2
                },
                new User("mock","mock")
                {
                    Id=userId3
                },
                new User("mock","mock")
                {
                    Id=userId4
                },
            };

            mockedUserRepository.Setup(r => r.GetAll()).Returns(users.AsQueryable());

            var mockedPassengerRouteRepository = new Mock<IRepository<PassengerRoute>>();

            var passengerRouteService = new PassengerRouteService(mockedPassengerRouteRepository.Object, mockedUserRepository.Object);

            var routes = new List<PassengerRouteForCreationDto>
            {
                new PassengerRouteForCreationDto
                {
                    UserId=routeId1
                },
                 new PassengerRouteForCreationDto
                {
                    UserId=routeId2
                },
                  new PassengerRouteForCreationDto
                {
                    UserId=routeId3
                },
                      new PassengerRouteForCreationDto
                {
                    UserId=routeId4
                }
            };
            //Act
            var actual = passengerRouteService.RoutesUsersExist(routes);
            //Assert
            Assert.Equal(expected, actual);
        }

    }
}

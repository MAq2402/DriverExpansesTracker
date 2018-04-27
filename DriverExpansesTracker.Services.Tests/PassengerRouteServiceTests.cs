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
        [InlineData("1","2","3","4")]
        [InlineData("michal", "olek", "ola", "dawid")]
        [InlineData("Xd", "xd", "xD", "XD")]
        public void SameUserForMultipleRoutes_ShouldReturnFalse(string id1,string id2,string id3 ,string id4)
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
            var expected = false;
            var actual = passengerRouteService.SameUserForMultipleRoutes(routes);

            //Assert
            Assert.Equal(expected, actual);     
        }

        [Theory]
        [InlineData("1", "2", "3", "1")]
        [InlineData("michal", "olek", "ola", "olek")]
        [InlineData("Xd", "xd", "xD", "xD")]
        public void SameUserForMultipleRoutes_ShouldReturnTrue(string id1, string id2, string id3, string id4)
        {
            //Arange
            var mockedPassengerRouteRepository = new Mock<IRepository<PassengerRoute>>();

            var mockedUserRepository = new Mock<IRepository<User>>();

            var passengerRouteService = new PassengerRouteService(
                mockedPassengerRouteRepository.Object
                , mockedUserRepository.Object);

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
            var expected = true;
            var actual = passengerRouteService.SameUserForMultipleRoutes(routes);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1","2","3","4", "1", "2", "3", "4")]
        [InlineData("1", "2", "3", "4", "1", "1", "1", "1")]
        [InlineData("1", "2", "3", "4", "1", "1", "2", "2")]
        public void RoutesUsersExist_ShouldReturnTrue(string userId1,string userId2, string userId3, string userId4,
                                     string routeId1, string routeId2, string routeId3, string routeId4)
        {
            //Arange
            var mockedUserRepository = new Mock<IRepository<User>>();
            var users = new List<User>
            {
                new User
                {
                    Id=userId1
                },
                new User
                {
                    Id=userId2
                },
                new User
                {
                    Id=userId3
                },
                new User
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

            var expected = true;
            var actual = passengerRouteService.RoutesUsersExist(routes);
            //Assert

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", "2", "3", "3", "1", "2", "3", "4")]
        [InlineData("3", "2", "3", "4", "1", "2", "3", "4")]
        [InlineData("1", "5", "3", "4", "1", "1", "2", "2")]
        public void RoutesUsersExist_ShouldReturnFalse(string userId1, string userId2, string userId3, string userId4,
                                     string routeId1, string routeId2, string routeId3, string routeId4)
        {
            //Arange
            var mockedUserRepository = new Mock<IRepository<User>>();
            var users = new List<User>
            {
                new User
                {
                    Id=userId1
                },
                new User
                {
                    Id=userId2
                },
                new User
                {
                    Id=userId3
                },
                new User
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

            var expected = false;
            var actual = passengerRouteService.RoutesUsersExist(routes);
            //Assert

            Assert.Equal(expected, actual);
        }
    }
}

using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.User;
using DriverExpansesTracker.Services.Services;
using DriverExpansesTracker.Services.Tests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Services.Tests
{
    public class UserServiceTests:MapperFixture
    {
        [Fact]
        public void GetPagedUsersTest()
        {
            //Arrange
            var mockedUserRepository = new Mock<IRepository<User>>();

            var collection = new List<User>()
            {
                new User
                {
                    UserName = "MAq",
                    FirstName = "Michał",
                    LastName = "Miciak"
                },
                new User
                {
                    UserName = "kowal",
                    FirstName = "Jan",
                    LastName = "Kowalski"
                },
                new User
                {
                    UserName = "adi",
                    FirstName = "Adrian",
                    LastName = "Nowak"
                }, new User
                {
                    UserName = "trol",
                    FirstName = "Michał",
                    LastName = "Zembiński"
                },
                new User
                {
                    UserName = "xD",
                    FirstName = "Wojtek",
                    LastName = "Michalski"
                },
                new User
                {
                    UserName = "michalwojtekkowal",
                    FirstName = "Michalina",
                    LastName = "Wojtkowska"
                }
            };

            mockedUserRepository.Setup(r => r.GetAll()).Returns(collection.AsQueryable());

            var userService = new UserService(mockedUserRepository.Object);

            var resourceParams = new ResourceParameters()
            {
                Search = "Micha"
            };


            //Act
            var actual = userService.GetPagedUsers(resourceParams);

            //Assert
            Assert.Collection(actual, user1 => Assert.Contains("Miciak", user1.LastName),
                                      user2 => Assert.Contains("Zembiński", user2.LastName),
                                      user3 => Assert.Contains("Michalski", user3.LastName),
                                      user4 => Assert.Contains("Wojtkowska", user4.LastName));

        }//todo Add more tests
    }
}

﻿using DriverExpansesTracker.Repository.Entities;
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
        private List<User> collection;
        private Mock<IRepository<User>> mockedUserRepository;

        public UserServiceTests()
        {
            collection = new List<User>()
            {
                new User("Michał","Miciak")
                {
                    UserName = "MAq"
                },
                new User("Jan","Kowalski")
                {
                    UserName = "kowal"
                },
                new User("Adrian","Nowak")
                {
                    UserName = "adi"
                }, new User("Michał","Zembiński")
                {
                    UserName = "trol"
                },
                new User("Wojtek","Michalski")
                {
                    UserName = "xD"
                },
                new User("Michalina","Wojtkowska")
                {
                    UserName = "michalwojtekkowal"
                }
            };

            mockedUserRepository = new Mock<IRepository<User>>();

            mockedUserRepository.Setup(r => r.GetAll()).Returns(collection.AsQueryable());
        }
        [Fact]
        public void GetPagedUsersTest_1()
        {
            //Arrange

            var userService = new UserService(mockedUserRepository.Object);

            var resourceParams = new ResourceParameters()
            {
                Search = "a"
            };


            //Act
            var actual = userService.GetPagedUsers(resourceParams);

            //Assert
            Assert.Collection(actual, user => Assert.Contains("Miciak", user.LastName),
                                      user => Assert.Contains("Kowalski", user.LastName),
                                      user => Assert.Contains("Nowak", user.LastName),
                                      user => Assert.Contains("Zembiński", user.LastName),
                                      user => Assert.Contains("Michalski", user.LastName),
                                      user => Assert.Contains("Wojtkowska", user.LastName));

        }

        [Fact]
        public void GetPagedUsersTest_2()
        {
            //Arrange
            var userService = new UserService(mockedUserRepository.Object);

            var resourceParams = new ResourceParameters()
            {
                Search = "qq"
            };


            //Act
            var actual = userService.GetPagedUsers(resourceParams);

            //Assert
            Assert.Empty(actual);

        }

        [Fact]
        public void GetPagedUsersTest_3()
        {
            //Arrange
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

        }
    }
}

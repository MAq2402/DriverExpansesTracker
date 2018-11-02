using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Services.Tests
{
    public class PaymentServiceTests
    {
        private Mock<IRepository<User>> mockedUserRepository;
        public PaymentServiceTests()
        {
            mockedUserRepository = new Mock<IRepository<User>>();
        }
        [Fact]
        public void AddPaymentsTestShouldThrowException()
        {
            //Arrange
            var mockedPaymentRepository = new Mock<IRepository<Payment>>();

            mockedPaymentRepository.Setup(r => r.Save()).Returns(false);

            var journey = new Journey("mock","mock",1,1,"mock");

            var paymentService = new PaymentService(mockedPaymentRepository.Object,mockedUserRepository.Object);

            //Act
            Action action = () => paymentService.AddPayments(journey);

            //Assert
            Assert.Throws<Exception>(action);
        }

        [Fact]

        public void AddPaymentsTest_1()
        {
            //Assert
            var mockedPaymentRepository = new Mock<IRepository<Payment>>();

            mockedPaymentRepository.Setup(r => r.Save()).Returns(true);

            var journey = new Journey("mock", "mock", 1, 1, "mock");

            var paymentService = new PaymentService(mockedPaymentRepository.Object, mockedUserRepository.Object);

            //Act
            var result = paymentService.AddPayments(journey);

            //Assert
            Assert.Empty(result);
        }

        [Fact]

        public void AddPaymentsTest_2()
        {
            //Arrange
            var mockedPaymentRepository = new Mock<IRepository<Payment>>();

            mockedPaymentRepository.Setup(r => r.Save()).Returns(true);

            var journey = new Journey("mock", "mock", 1, 1, "mock");

            var ps1 = new PassengerRoute("mock", 1, "mock", 1);
            var ps2 = new PassengerRoute("mock", 1, "mock", 1);
            var ps3 = new PassengerRoute("mock", 1, "mock", 1);

            ps1.SetTotalPrice(50);
            ps2.SetTotalPrice(100);
            ps3.SetTotalPrice(150);

            journey.PassengerRoutes.Add(ps1);
            journey.PassengerRoutes.Add(ps2);
            journey.PassengerRoutes.Add(ps3);

            var paymentService = new PaymentService(mockedPaymentRepository.Object, mockedUserRepository.Object);

            //Act
            var actual = paymentService.AddPayments(journey);

            //Assert
            Assert.Collection(actual, p => Assert.Equal(50,p.Amount),
                                      p => Assert.Equal(100, p.Amount),
                                      p => Assert.Equal(150, p.Amount));
        }
    }
}

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
        [Fact]
        public void AddPaymentsTestShouldThrowException()
        {
            //Arrange
            var mockedPaymentRepository = new Mock<IRepository<Payment>>();

            mockedPaymentRepository.Setup(r => r.Save()).Returns(false);

            var journey = new Journey();

            var paymentService = new PaymentService(mockedPaymentRepository.Object);

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

            var journey = new Journey();

            var paymentService = new PaymentService(mockedPaymentRepository.Object);

            //Act
            var result = paymentService.AddPayments(journey);

            //Assert
            Assert.Empty(result);
        }

        [Fact]

        public void AddPaymentsTest_2()
        {
            //Assert
            var mockedPaymentRepository = new Mock<IRepository<Payment>>();

            mockedPaymentRepository.Setup(r => r.Save()).Returns(true);

            var journey = new Journey();

            var ps1 = new PassengerRoute
            {
                TotalPrice = 50
            };
            var ps2 = new PassengerRoute
            {
                TotalPrice = 100
            };
            var ps3 = new PassengerRoute
            {
                TotalPrice = 150
            };

            journey.PassengerRoutes.Add(ps1);
            journey.PassengerRoutes.Add(ps2);
            journey.PassengerRoutes.Add(ps3);

            var paymentService = new PaymentService(mockedPaymentRepository.Object);

            //Act
            var actual = paymentService.AddPayments(journey);

            //Assert
            Assert.Collection(actual, p => Assert.Equal(50,p.Amount),
                                      p => Assert.Equal(100, p.Amount),
                                      p => Assert.Equal(150, p.Amount));
        }
    }
}

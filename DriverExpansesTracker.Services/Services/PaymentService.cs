using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private IRepository<Payment> _paymentRepository;

        public PaymentService(IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public PaymentDto GetPayment(string userId, int id)
        {
            var payment = _paymentRepository.FindSingleBy(p => p.Id == id && (p.ReceiverId == userId || p.PayerId == userId));

            return Mapper.Map<PaymentDto>(payment);
        }

        public PaymentDto GetPayment(string userId, int journeyId, int id)
        {
            var payment = _paymentRepository.FindSingleBy(p => p.Id == id && p.JourneyId==journeyId && (p.ReceiverId == userId || p.PayerId == userId));

            return Mapper.Map<PaymentDto>(payment);
        }

        public PagedList<PaymentDto> GetPagedPayments(string userId, ResourceParameters resourceParameters)
        {
            var paymentsFromRepo = _paymentRepository.FindBy(p => p.ReceiverId == userId || p.PayerId == userId)
                                              .OrderByDescending(p => p.DateTime);

            var payments = Mapper.Map<IEnumerable<PaymentDto>>(paymentsFromRepo).AsQueryable();

            return new PagedList<PaymentDto>(payments, resourceParameters.PageNumber, resourceParameters.PageSize);               
        }
        public IEnumerable<Payment> AddPayments(Journey journey)
        {
            var payments = new List<Payment>();

            var routes = journey.PassengerRoutes;

            foreach (var route in routes)
            {
                var payment = new Payment
                {
                    ReceiverId = journey.UserId,
                    PayerId = route.UserId,
                    JourneyId = journey.Id,
                    Amount = route.TotalPrice,
                };
                payments.Add(payment);
            }

            _paymentRepository.AddRange(payments);

            if(!_paymentRepository.Save())
            {
                throw new Exception("Could not save payments");
            }

            return payments;
        }

        public PagedList<PaymentDto> GetPagedPaymentsByJourneys(string userId, int journeyId, ResourceParameters resourceParameters)
        {
            var paymentsFromRepo = _paymentRepository.FindBy(p => (p.PayerId == userId || p.ReceiverId == userId) && p.JourneyId == journeyId)
                                                      .OrderByDescending(p=>p.DateTime);

            var paymentsDtos = Mapper.Map<IEnumerable<PaymentDto>>(paymentsFromRepo);

            return new PagedList<PaymentDto>(paymentsDtos.AsQueryable(), resourceParameters.PageNumber, resourceParameters.PageSize);
        }
    }
}

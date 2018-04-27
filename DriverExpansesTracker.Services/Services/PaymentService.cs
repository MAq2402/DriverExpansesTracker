using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
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

        public IEnumerable<PaymentDto> GetPayments(string userId)
        {
            var payments = _paymentRepository.FindBy(p => p.ReceiverId == userId || p.PayerId == userId)
                                              .OrderByDescending(p => p.DateTime);

            return Mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public IEnumerable<PaymentDto> GetPayments(string userId, int journeyId)
        {
            var payments = _paymentRepository.FindBy(p => p.JourneyId==journeyId&&( p.ReceiverId == userId || p.PayerId == userId))
                                               .OrderByDescending(p => p.DateTime);

            return Mapper.Map<IEnumerable<PaymentDto>>(payments);
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
    }
}

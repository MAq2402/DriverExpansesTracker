using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public class PaymentService:IPaymentService
    {
        private IRepository<Payment> _paymentRepository;

        public PaymentService(IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public object GetPayments(string userId, int journeyId)
        {
            throw new NotImplementedException();
        }

        public object GetPayments(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

using DriverExpansesTracker.Services.Models.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using DriverExpansesTracker.Repository.Entities;

namespace DriverExpansesTracker.Services.Services
{
    public interface IPaymentService
    {
        PaymentDto GetPayment(string userId, int id);
        IEnumerable<PaymentDto> GetPayments(string userId);
        IEnumerable<PaymentDto> GetPayments(string userId, int journeyId);
        PaymentDto GetPayment(string userId, int journeyId, int id);
        IEnumerable<Payment> AddPayments(Journey journey);
    }
}

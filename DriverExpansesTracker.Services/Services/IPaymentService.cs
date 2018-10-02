using DriverExpansesTracker.Services.Models.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Helpers;

namespace DriverExpansesTracker.Services.Services
{
    public interface IPaymentService
    {
        PaymentDto GetPayment(string userId, int id);
        PagedList<PaymentDto> GetPagedPayments(string userId,ResourceParameters resourceParameters);
        PagedList<PaymentDto> GetPagedPaymentsByJourneys(string userId, int journeyId, ResourceParameters resourceParameters);
        PaymentDto GetPayment(string userId, int journeyId, int id);
        IEnumerable<Payment> AddPayments(Journey journey);
        
    }
}

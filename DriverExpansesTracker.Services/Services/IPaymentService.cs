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
        PagedList<Payment> GetPagedPayments(string userId,ResourceParameters resourceParameters);
        IEnumerable<PaymentDto> GetPayments(string userId, int journeyId);
        PaymentDto GetPayment(string userId, int journeyId, int id);
        IEnumerable<Payment> AddPayments(Journey journey);
        IEnumerable<PaymentDto> GetPayments(PagedList<Payment> pagedList);
        
    }
}

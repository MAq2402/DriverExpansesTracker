using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface IPaymentService
    {
        object GetPayments(string userId, int journeyId);
        object GetPayments(string userId);
    }
}

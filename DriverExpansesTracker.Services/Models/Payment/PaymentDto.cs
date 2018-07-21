using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string PayerId { get; set; }
        public string ReceiverId { get; set; }
        public int JourneyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPayed { get; set; } = false;
    }
}

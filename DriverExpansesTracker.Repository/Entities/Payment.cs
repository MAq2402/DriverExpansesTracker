using DriverExpansesTracker.Repository.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class Payment:Entity
    {
        

        [ForeignKey("Payer")]
        public string PayerId { get; private set; }

        [Required]
        public User Payer { get; private set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; private set; }

        [Required]
        public User Receiver { get; private set; }

        [ForeignKey("Journey")]
        public int JourneyId { get; private set; }

        [Required]
        public Journey Journey { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
        public bool IsPayed { get; private set; } = false;

        private Payment()
        {

        }

        public Payment(string payerId,string receiverId, int journeyId, decimal amount )
        {
            PayerId = payerId;
            ReceiverId = receiverId;
            JourneyId = journeyId;
            DateTime = DateTime.Now;

            SetAmount(amount);
        }

        private void SetAmount(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Payment amount is less than 0");
            }

            Amount = amount;
        }

        public void AcceptPayment()
        {
            if (IsPayed)
            {
                throw new InvalidOperationException("Payment has been already accepted");
            }

            IsPayed = true;
        }
    }
}
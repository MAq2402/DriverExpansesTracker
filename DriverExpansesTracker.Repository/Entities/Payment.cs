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
        public Payment()
        {
            DateTime = DateTime.Now;
        }

        [ForeignKey("Payer")]
        public string PayerId { get; set; }

        [Required]
        public User Payer { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        [Required]
        public User Receiver { get; set; }

        [ForeignKey("Journey")]
        public int JourneyId { get; set; }

        [Required]
        public Journey Journey { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPayed { get; set; } = false;

    }
}
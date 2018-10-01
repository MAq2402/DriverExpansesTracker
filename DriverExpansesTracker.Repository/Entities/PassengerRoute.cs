using DriverExpansesTracker.Repository.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class PassengerRoute:Entity
    {
        public PassengerRoute()
        {
            DateTime = DateTime.Now;
        }
        public string Destination { get; set; }

        public string Start { get; set; }

        public int Length { get; set; }

        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required,ForeignKey("Journey")]
        public int JourneyId { get; set; }

        [Required]
        public Journey Journey { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
        public DateTime DateTime { get; set; }

    }
}
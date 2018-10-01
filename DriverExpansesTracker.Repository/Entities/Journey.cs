using DriverExpansesTracker.Repository.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class Journey:Entity
    {
        public Journey()
        {
            DateTime = DateTime.Now;
        }

        public string Destination { get; set; }

        public string Start { get; set; }

        public int Length { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        public List<PassengerRoute> PassengerRoutes { get; set; } = new List<PassengerRoute>();

        [ForeignKey("User")]
        public string UserId { get; set; }

 
        public User User { get; set; }

        public DateTime DateTime { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
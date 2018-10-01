
using DriverExpansesTracker.Repository.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [InverseProperty("Receiver")]
        public List<Payment> ReceivedPayments { get; set; } = new List<Payment>();

        [InverseProperty("Payer")]
        public List<Payment> PayedPayments { get; set; } = new List<Payment>();
        public List<PassengerRoute> PassengerRoutes { get; set; } = new List<PassengerRoute>();
        public List<Car> Cars { get; set; } =  new List<Car>();

        public List<Journey> Journeys { get; set; } = new List<Journey>();
        public decimal ToPay { get; set; }
        public decimal ToReceive { get; set; }
        public decimal Payed { get; set; }
        public decimal Received { get; set; }
    }
}
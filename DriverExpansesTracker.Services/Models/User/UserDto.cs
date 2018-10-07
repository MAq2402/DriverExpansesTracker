using RiskFirst.Hateoas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Models.User
{
    public class UserDto : LinkContainer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public decimal ToPay { get; set; }
        public decimal ToReceive { get; set; }
        public decimal Payed { get; set; }
        public decimal Received { get; set; }

        //public List<Payment> ReceivedPayments { get; set; } = new List<Payment>();
        //public List<Payment> PayedPayments { get; set; } = new List<Payment>();
        //public List<PassengerRouteDto> UserJourneys { get; set; } = new List<PassengerRouteDto>();
        //public List<CarDto> Cars { get; set; } = new List<CarDto>();
    }
}

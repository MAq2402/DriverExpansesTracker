using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Models.PassengerRoute
{
    public class PassengerRouteDto
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public string Start { get; set; }

        public int Length { get; set; }

        public string UserId { get; set; }

        public int JourneyId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}

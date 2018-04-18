using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Journey
{
    public class JourneyDto
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public string Start { get; set; }

        public int Length { get; set; }

        public int CarId { get; set; }
        public string UserId { get; set; }

        //List<PassengerRouteDto> PassengerRoutes { get; set; } = new List<PassengerRouteDto>();

        public DateTime DateTime { get; set; }

        public decimal TotalPrice { get; set; }
    }
}

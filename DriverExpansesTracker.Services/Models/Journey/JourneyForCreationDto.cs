using DriverExpansesTracker.Services.Models.PassengerRoute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Journey
{
    public class JourneyForCreationDto
    {
        [Required]
        public string Destination { get; set; }

        [Required]
        public string Start { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public decimal PriceForLiter { get; set; }
        public ICollection<PassengerRouteForCreationDto> Routes { get; set; } = new List<PassengerRouteForCreationDto>();
    }
}

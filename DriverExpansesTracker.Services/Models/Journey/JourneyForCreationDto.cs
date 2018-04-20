using DriverExpansesTracker.Services.Models.PassengerRoute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Journey
{
    public class JourneyForCreationDto
    {
        [Required, MaxLength(50)]
        public string Destination { get; set; }

        [Required, MaxLength(50)]
        public string Start { get; set; }

        [Required, Range(1, 400000)]
        public int Length { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required, Range(0, 200)]
        public decimal PriceForLiter { get; set; }
        public ICollection<PassengerRouteForCreationDto> Routes { get; set; } = new List<PassengerRouteForCreationDto>();
    }
}

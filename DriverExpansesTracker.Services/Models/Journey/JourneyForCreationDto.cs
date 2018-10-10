using DriverExpansesTracker.Services.Models.PassengerRoute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Journey
{
    public class JourneyForCreationDto
    {
        [Required(ErrorMessage = "Nie podano celu podróży")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Nie podano miejsca rozpoczęcia podróży")]
        public string Start { get; set; }

        [Required(ErrorMessage = "Nie podano długości podróży")]
        public double Length { get; set; }

        [Required(ErrorMessage = "Nie podano ceny za litr paliwa")]
        public decimal PriceForLiter { get; set; }

        public int CarId { get; set; }

        public string UserId { get; set; }
        public ICollection<PassengerRouteForCreationDto> Routes { get; set; } = new List<PassengerRouteForCreationDto>();
    }
}

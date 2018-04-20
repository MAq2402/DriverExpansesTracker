using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.PassengerRoute
{
    public class PassengerRouteForCreationDto
    {
        [Required, MaxLength(50)]
        public string Destination { get; set; }

        [Required, MaxLength(50)]
        public string Start { get; set; }

        [Required, Range(1, 400000)]
        public int Length { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

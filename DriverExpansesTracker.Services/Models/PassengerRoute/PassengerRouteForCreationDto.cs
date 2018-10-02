using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.PassengerRoute
{
    public class PassengerRouteForCreationDto
    {
        [Required]
        public string Start { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

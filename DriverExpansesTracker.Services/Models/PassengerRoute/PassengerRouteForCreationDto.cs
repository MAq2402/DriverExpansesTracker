using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.PassengerRoute
{
    public class PassengerRouteForCreationDto
    {
        [Required(ErrorMessage = "Nie podano miejsca w którym wsiadł pasażer")]
        public string Start { get; set; }

        [Required(ErrorMessage = "Nie podano długości podróży pasażera")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Nie podano pasażera")]
        public string UserId { get; set; }
    }
}

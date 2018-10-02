using DriverExpansesTracker.Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Car
{
    public class CarForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double FuelConsumption100km { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

    }
}

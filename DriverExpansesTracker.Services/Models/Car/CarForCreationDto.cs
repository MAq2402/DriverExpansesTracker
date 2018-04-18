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
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public double FuelConsumption100km { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Car
{
    public class CarForUpdateDto
    {
        [Required(ErrorMessage = "Nie podano nazwy dla samochodu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nie podano spalania")]
        public double FuelConsumption100km { get; set; }

        [Required(ErrorMessage = "Nie podano typu paliwa")]
        public string FuelType { get; set; }
    }
}

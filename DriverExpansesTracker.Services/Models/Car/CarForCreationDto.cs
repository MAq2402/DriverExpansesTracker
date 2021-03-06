﻿using DriverExpansesTracker.Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Car
{
    public class CarForCreationDto
    {
        [Required(ErrorMessage = "Nie podano nazwy dla samochodu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nie podano spalania")]
        [Range(double.Epsilon,double.MaxValue,ErrorMessage ="Spalanie musi mieć wartośc powyżej 0")]
        public double FuelConsumption100km { get; set; }

        [Required(ErrorMessage = "Nie podano typu paliwa")]
        public FuelType FuelType { get; set; }

        public string UserId { get; set; }

    }
}

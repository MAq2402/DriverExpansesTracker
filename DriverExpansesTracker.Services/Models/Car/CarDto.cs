﻿using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Car
{
    public class CarDto:LinkContainer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public double FuelConsumption100km { get; set; }
        public string FuelType { get; set; }
        public bool Active { get; set; }
        //public List<Journey> Journeys { get; set; } = new List<Journey>();
    }
}

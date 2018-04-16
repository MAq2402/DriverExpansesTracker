using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Models.Car
{
    public class CarDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double FuelConsumption100km { get; set; }
        public string FuelType { get; set; }
        //public List<Journey> Journeys { get; set; } = new List<Journey>();
    }
}

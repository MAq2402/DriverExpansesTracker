using DriverExpansesTracker.Repository.Entities.Base;
using DriverExpansesTracker.Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
  
    public class Car:Entity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }
        public string Name { get; set; }
        public double FuelConsumption100km { get; set; }
        public List<Journey> Journeys { get; set; } = new List<Journey>();
        public FuelType FuelType { get; set; }
        public bool Active { get; set; } = true;

    }
}
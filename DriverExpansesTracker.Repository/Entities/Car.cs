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
        [Required,ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required,Range(1,100)]
        public double FuelConsumption100km { get; set; }
        public List<Journey> Journeys { get; set; } = new List<Journey>();

        [Required]
        public FuelType FuelType { get; set; }

        public bool Active { get; set; } = true;

    }
}
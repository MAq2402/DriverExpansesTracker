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
        public string UserId { get; private set; }
        [Required]
        public User User { get; private set; }
        public string Name { get; private set; }
        public double FuelConsumption100km { get; private set; }
        public List<Journey> Journeys { get; private set; } = new List<Journey>();
        public FuelType FuelType { get; private set; }
        public bool Active { get; private set; } = true;

        private Car()
        {

        }

        public void Disactivate()
        {
            if (!Active)
            {
                throw new InvalidOperationException("Car has already been disactivated");
            }

            Active = false;
        }


        public Car(string userId,string name, double fuelConsumption100km, FuelType fuelType)
        {
            UserId = userId;
            FuelType = FuelType;

            SetName(name);
            SetFuelConsumption(fuelConsumption100km);
        }

        private void SetFuelConsumption(double fuelConsumption100km)
        {
            if(fuelConsumption100km < 0)
            {
                throw new ArgumentException("Fuel consumption is less than 0");
            }

            FuelConsumption100km = fuelConsumption100km;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Car name is null or empty");
            }

            Name = name;
        }

    }
}
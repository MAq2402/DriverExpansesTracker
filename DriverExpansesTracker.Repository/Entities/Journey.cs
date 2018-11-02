using DriverExpansesTracker.Repository.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class Journey:Entity
    {
        public string Destination { get; private set; }

        public string Start { get; private set; }

        public double Length { get; private set; }

        [ForeignKey("Car")]
        public int CarId { get; private set; }

        [Required]
        public Car Car { get; private set; }
        public List<PassengerRoute> PassengerRoutes { get; private set; } = new List<PassengerRoute>();

        [ForeignKey("User")]
        public string UserId { get; private set; }

        [Required]
        public User User { get; private set; }

        public DateTime DateTime { get; private set; }

        public decimal TotalPrice { get; private set; }

        private Journey()
        {
            DateTime = DateTime.Now;
        }

        public Journey(string destination, string start, double length, int carId,string userId)
        {
            DateTime = DateTime.Now;
            CarId = carId;
            UserId = userId;

            SetDestination(destination);
            SetStart(start);
            SetLength(length);
        }

        public void SetTotalPrice(double fuelConsumption100Km, decimal priceForLiter)
        {
            if(fuelConsumption100Km <= 0 || priceForLiter <= 0)
            {
                throw new ArgumentException();
            }

            TotalPrice = Math.Round(Convert.ToDecimal(fuelConsumption100Km * Length * (double)priceForLiter / 100),2);
        }

        private void SetLength(double length)
        {
            if (length <= 0)
            {
                throw new ArgumentException();
            }
            Length = length;
        }

        private void SetStart(string start)
        {
            if (string.IsNullOrEmpty(start))
            {
                throw new ArgumentNullException();
            }
            Start = start;
        }

        private void SetDestination(string destination)
        {
            if (string.IsNullOrEmpty(destination))
            {
                throw new ArgumentNullException();
            }
            Destination = destination;
        }
    }
}
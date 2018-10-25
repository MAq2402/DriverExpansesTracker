using DriverExpansesTracker.Repository.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class PassengerRoute:Entity
    {
        public string Destination { get; private set; }

        public string Start { get; private set; }

        public int Length { get; private set; }

        public string UserId { get; private set; }

        [Required]
        public User User { get; private set; }

        [Required,ForeignKey("Journey")]
        public int JourneyId { get; private set; }

        [Required]
        public Journey Journey { get; private set; }

        public decimal TotalPrice { get; private set; }
        public DateTime DateTime { get; private set; }

        public PassengerRoute(string destination, string start, int length, string userId, int journeyId)
        {
            UserId = userId;
            JourneyId = journeyId;

            DateTime = DateTime.Now;

            SetStart(start);
            SetLength(length);
        }

        public void SetTotalPrice(decimal totalPrice)
        {
            if (totalPrice < 0)
            {
                throw new ArgumentException("Route total price is less than 0");
            }

            TotalPrice = totalPrice;
        }

        private void SetLength(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("Route length is less than 0");
            }

            Length = length;
        }

        private void SetStart(string start)
        {
            if (string.IsNullOrEmpty(start))
            {
                throw new ArgumentNullException("Destination is not provided");
            }

            Start = start;
        }

        public void SetDestination(string destination)
        {
            if (string.IsNullOrEmpty(destination))
            {
                throw new ArgumentNullException("Destination is not provided");
            }

            Destination = destination;
        }

        private PassengerRoute()
        {

        }

    }
}
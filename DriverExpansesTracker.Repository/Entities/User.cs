
using DriverExpansesTracker.Repository.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriverExpansesTracker.Repository.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        [InverseProperty("Receiver")]
        public List<Payment> ReceivedPayments { get; private set; } = new List<Payment>();

        [InverseProperty("Payer")]
        public List<Payment> PayedPayments { get; private set; } = new List<Payment>();
        public List<PassengerRoute> PassengerRoutes { get; private set; } = new List<PassengerRoute>();
        public List<Car> Cars { get; private set; } = new List<Car>();

        public List<Journey> Journeys { get; private set; } = new List<Journey>();
        public decimal ToPay { get; private set; }
        public decimal ToReceive { get; private set; }
        public decimal Payed { get; private set; }
        public decimal Received { get; private set; }

        private User()
        {

        }
        public User(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException("Last name is not provided");
            }

            LastName = lastName;
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException("Last name is not provided");
            }

            FirstName = firstName;
        }


        public void UpdatePayments()
        {
            Received = ReceivedPayments.Where(p => p.IsPayed).Sum(p => p.Amount);
            Payed = PayedPayments.Where(p => p.IsPayed).Sum(p => p.Amount);
            ToReceive = ReceivedPayments.Where(p => !p.IsPayed).Sum(p => p.Amount);
            ToPay = PayedPayments.Where(p => !p.IsPayed).Sum(p => p.Amount);
        }
    }
}
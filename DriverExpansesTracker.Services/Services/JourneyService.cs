using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Models.Journey;

namespace DriverExpansesTracker.Services.Services
{
    public class JourneyService : IJourneyService
    {
        private IRepository<Journey> _journeyRepository;

        public JourneyService(IRepository<Journey> journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        public JourneyDto GetJourney(string userId, int id)
        {
            var journey = _journeyRepository.FindSingleBy(j => j.UserId == userId&&j.Id==id);
            return Mapper.Map<JourneyDto>(journey);
        }

        public JourneyDto GetJourney(string userId, int carId, int id)
        {
            var journey = _journeyRepository.FindSingleBy(j => j.UserId == userId && j.CarId == carId&&j.Id==id);
            return Mapper.Map<JourneyDto>(journey);
        }

        public IEnumerable<JourneyDto> GetJourneys(string userId)
        {
            var journeys = _journeyRepository.FindBy(j => j.UserId == userId);
            return Mapper.Map<IEnumerable<JourneyDto>>(journeys);
        }

        public IEnumerable<JourneyDto> GetJourneys(string userId, int carId)
        {
            var journeys = _journeyRepository.FindBy(j => j.UserId == userId&&j.CarId==carId);
            return Mapper.Map<IEnumerable<JourneyDto>>(journeys);
        }

        public bool JourneyExists(string userId, int id)
        {
           if(_journeyRepository.FindBy(j=>j.UserId==userId&&j.Id==id).Any())
            {
                return true;
            }
            return false;
        }
        private void CalculatePrices(Journey journey, double fuelConsumption100km, double priceForLiter)
        {
            journey.TotalPrice = Convert.ToDecimal(fuelConsumption100km * journey.Length * priceForLiter / 100);

            var listOfRoutes = journey.PassengerRoutes.OrderBy(pr => pr.Length).ToList();

            int numberOfPassengers = listOfRoutes.Count();

            var currentCost = 0m;// represents cost of current length

            var previousLength = 0; //represents length for which we have already calculated TotalPrice 

            int i = 0;

            while (numberOfPassengers > i)
            {
                var currentLength = listOfRoutes[i].Length;

                var listOfRoutesWithSameLength = listOfRoutes.Skip(i).TakeWhile(pr => pr.Length == currentLength).ToList();

                currentLength -= previousLength; // subtructing from currentLength to calculate currentCost

                currentCost += Convert.ToDecimal((fuelConsumption100km * currentLength * priceForLiter) / (100 * (numberOfPassengers - i + 1))); //+1 becouse of user who creates journey

                foreach (var route in listOfRoutesWithSameLength)
                {
                    route.TotalPrice = currentCost;
                }

                i += listOfRoutesWithSameLength.Count();

                previousLength += currentLength;
            }
        }
    }
}

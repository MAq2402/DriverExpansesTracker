using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.Car;
using DriverExpansesTracker.Services.Models.Journey;

namespace DriverExpansesTracker.Services.Services
{
    public class JourneyService : IJourneyService
    {
        private IRepository<Journey> _journeyRepository;
        private IRepository<Car> _carRepository;

        public JourneyService(IRepository<Journey> journeyRepository, IRepository<Car> carRepository)
        {
            _journeyRepository = journeyRepository;
            _carRepository = carRepository;

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


        public bool JourneyExists(string userId, int id)
        {
            return _journeyRepository.FindBy(j => j.UserId == userId && j.Id == id).Any();
 
        }

        public Journey AddJourney(string userId, JourneyForCreationDto journey,CarDto car)
        {
            var journeyToSave = Mapper.Map<Journey>(journey);

            if (car == null)
            {
                throw new Exception("Car does not exist");
            }

            journeyToSave.PassengerRoutes.ForEach(pr => pr.DateTime = journeyToSave.DateTime);

            journeyToSave.PassengerRoutes.ForEach(pr => pr.Destination = journeyToSave.Destination);
            
            journeyToSave.UserId = userId;

            journeyToSave.CarId = car.Id;

            _journeyRepository.Add(journeyToSave);

            if (!_journeyRepository.Save())
            {
                throw new Exception("Could not save journey");
            }

            return journeyToSave;
        }

        public JourneyDto GetJourney(Journey journey)
        {
            return Mapper.Map<JourneyDto>(journey);
        }

        public void SetTotalPrices(Journey journey, double fuelConsumption100Km, decimal priceForLiter)
        {
            journey.TotalPrice = Convert.ToDecimal(fuelConsumption100Km * journey.Length * (double)priceForLiter / 100);

            var listOfRoutes = journey.PassengerRoutes.OrderBy(pr => pr.Length).ToList();

            var numberOfPassengers = listOfRoutes.Count();

            var currentCost = 0m;// represents cost of current length

            var previousLength = 0; //represents length for which we have already calculated TotalPrice 

            var i = 0;

            while (numberOfPassengers > i)
            {
                var currentLength = listOfRoutes[i].Length;

                var listOfRoutesWithSameLength = listOfRoutes.Skip(i).TakeWhile(pr => pr.Length == currentLength).ToList();

                currentLength -= previousLength; // subtructing from currentLength to calculate currentCost

                currentCost += Convert.ToDecimal((fuelConsumption100Km * currentLength * (double)priceForLiter) / (100 * (numberOfPassengers - i + 1))); //+1 becouse of user who creates journey

                foreach (var route in listOfRoutesWithSameLength)
                {
                    route.TotalPrice = currentCost;
                }

                i += listOfRoutesWithSameLength.Count();

                previousLength += currentLength;
            }

            if(!_journeyRepository.Save())
            {
                throw new Exception("Could not save total prices");
            }
        }

        public PagedList<JourneyDto> GetPagedJourneys(string userId, ResourceParameters resourceParameters, int carId)
        {
                var journeysFromRepo = _journeyRepository.FindBy(j => j.CarId == carId && j.UserId == userId);

                var journeysToReturn = Mapper.Map<IEnumerable<JourneyDto>>(journeysFromRepo);

                return new PagedList<JourneyDto>(journeysToReturn.AsQueryable(), resourceParameters.PageNumber, resourceParameters.PageSize);            
        }

        public PagedList<JourneyDto> GetPagedJourneys(string userId, ResourceParameters resourceParameters)
        {
            var journeysFromRepo = _journeyRepository.FindBy(j => j.UserId == userId);

            var journeysToReturn = Mapper.Map<IEnumerable<JourneyDto>>(journeysFromRepo);

            return new PagedList<JourneyDto>(journeysToReturn.AsQueryable(), resourceParameters.PageNumber, resourceParameters.PageSize);
        }

        public bool JourneyExists(int journeyId)
        {
            return _journeyRepository.FindBy(j => j.Id == journeyId).Any();
        }
    }
}

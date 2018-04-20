using System;
using System.Collections.Generic;
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
    }
}

using DriverExpansesTracker.Services.Models.Journey;
using System;
using System.Collections.Generic;
using System.Text;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Helpers;
using System.Linq;
using DriverExpansesTracker.Services.Models.Car;

namespace DriverExpansesTracker.Services.Services
{
    public interface IJourneyService
    {
        JourneyDto GetJourney(string userId, int id);
        JourneyDto GetJourney(string userId, int carId, int id);
        bool JourneyExists(string userId, int id);
        Journey AddJourney(string userId, JourneyForCreationDto journey,CarDto car);
        JourneyDto GetJourney(Journey journey);
        void SetTotalPrices(Journey journey, double fuelConsumption100Km, decimal priceForLiter);
        PagedList<JourneyDto> GetPagedJourneys(string userId, ResourceParameters resourceParameters, int carId);
        PagedList<JourneyDto> GetPagedJourneys(string userId, ResourceParameters resourceParameters);
        bool JourneyExists(int journeyId);
        void DeleteJourney(string userId, int id);
    }
}

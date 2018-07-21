﻿using DriverExpansesTracker.Services.Models.Journey;
using System;
using System.Collections.Generic;
using System.Text;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Helpers;
using System.Linq;

namespace DriverExpansesTracker.Services.Services
{
    public interface IJourneyService
    {
        IEnumerable<JourneyDto> GetJourneys(string userId);
        IEnumerable<JourneyDto> GetJourneys(string userId, int carId);
        JourneyDto GetJourney(string userId, int id);
        JourneyDto GetJourney(string userId, int carId, int id);
        bool JourneyExists(string userId, int id);
        Journey AddJourney(string userId, JourneyForCreationDto journey);
        JourneyDto GetJourney(Journey journey);
        void SetTotalPrices(Journey journey, double fuelConsumption100Km, decimal priceForLiter);
        //PagedList<Journey> GetPagedJourneys(string userId, JourneyResourceParameters resourceParameters);
        //IEnumerable<JourneyDto> GetJourneys(PagedList<Journey> pagedList);
    }
}

using DriverExpansesTracker.Services.Models.Journey;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface IJourneyService
    {
        IEnumerable<JourneyDto> GetJourneys(string userId);
        IEnumerable<JourneyDto> GetJourneys(string userId, int carId);
        JourneyDto GetJourney(string userId, int id);
        JourneyDto GetJourney(string userId, int carId, int id);
        bool JourneyExists(string userId, int id);
    }
}

using DriverExpansesTracker.Services.Models.PassengerRoute;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface IPassengerRouteService
    {
        IEnumerable<PassengerRouteDto> GetRoutes(string userId);
        IEnumerable<PassengerRouteDto> GetRoutes(string userId, int journeyId);
        PassengerRouteDto GetRoute(string userId, int journeyId, int id);
        PassengerRouteDto GetRoute(string userId, int id);
    }
}

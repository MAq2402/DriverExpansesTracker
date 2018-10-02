using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.PassengerRoute;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface IPassengerRouteService
    {
        PassengerRouteDto GetRoute(string userId, int journeyId, int id);
        PassengerRouteDto GetRoute(string userId, int id);
        bool RoutesUsersExist(IEnumerable<PassengerRouteForCreationDto> routes);
        bool SameUserForMultipleRoutes(IEnumerable<PassengerRouteForCreationDto> routes);
        PagedList<PassengerRouteDto> GetPagedRoutes(string userId, int journeyId, ResourceParameters resourceParameters);
        PagedList<PassengerRouteDto> GetPagedRoutes(string userId, ResourceParameters resourceParameters);
    }
}

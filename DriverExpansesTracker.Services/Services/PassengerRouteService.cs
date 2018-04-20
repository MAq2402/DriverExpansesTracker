using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Models.PassengerRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public class PassengerRouteService : IPassengerRouteService
    {
        private IRepository<PassengerRoute> _routeRepository;

        public PassengerRouteService(IRepository<PassengerRoute> routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public IEnumerable<PassengerRouteDto> GetRoutes(string userId)
        {
            var routes = _routeRepository.FindBy(r => r.UserId == userId)
                                         .OrderByDescending(p => p.DateTime);
            return Mapper.Map<IEnumerable<PassengerRouteDto>>(routes);
        }

        public IEnumerable<PassengerRouteDto> GetRoutes(string userId, int journeyId)
        {
            var routes = _routeRepository.FindBy(r => r.UserId == userId && r.JourneyId == journeyId)
                                         .OrderByDescending(p => p.DateTime);
            return Mapper.Map<IEnumerable<PassengerRouteDto>>(routes);
        }

        public PassengerRouteDto GetRoute(string userId, int journeyId, int id)
        {
            var route = _routeRepository.FindSingleBy(r => r.UserId == userId && r.JourneyId == journeyId && r.Id == id);

            return Mapper.Map<PassengerRouteDto>(route);
        }

        PassengerRouteDto IPassengerRouteService.GetRoute(string userId, int id)
        {
            var route = _routeRepository.FindSingleBy(r => r.UserId == userId && r.Id == id);

            return Mapper.Map<PassengerRouteDto>(route);
        }
    }
}

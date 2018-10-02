using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Helpers;
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
        private IRepository<User> _userRepository;

        public PassengerRouteService(IRepository<PassengerRoute> routeRepository,IRepository<User> userRepository)
        {
            _routeRepository = routeRepository;
            _userRepository = userRepository;
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

        public bool RoutesUsersExist(IEnumerable<PassengerRouteForCreationDto> routes)
        {       
            var users = _userRepository.GetAll();

            foreach (var route in routes)
            {
                if (!users.Any(u=>u.Id==route.UserId))
                {
                    return false;
                }
            }
            return true;
        }

        public bool SameUserForMultipleRoutes(IEnumerable<PassengerRouteForCreationDto> routes)
        {
            var userIds = new List<string>();

            foreach (var route in routes)
            {

                if (userIds.Any(x => x == route.UserId))
                {
                    return true;
                }

                userIds.Add(route.UserId);
            }
            return false;
        }

        public PagedList<PassengerRouteDto> GetPagedRoutes(string userId, int journeyId, ResourceParameters resourceParameters)
        {
            var routesFromRepo = _routeRepository.FindBy(r => r.UserId == userId && r.JourneyId == journeyId)
                                                 .OrderByDescending(r => r.DateTime);

            var routesDtos = Mapper.Map<IEnumerable<PassengerRouteDto>>(routesFromRepo);

            return new PagedList<PassengerRouteDto>(routesDtos.AsQueryable(), resourceParameters.PageNumber, resourceParameters.PageSize);
        }

        public PagedList<PassengerRouteDto> GetPagedRoutes(string userId, ResourceParameters resourceParameters)
        {
            var routesFromRepo = _routeRepository.FindBy(r => r.UserId == userId)
                                                 .OrderByDescending(r=>r.DateTime);

            var routesDtos = Mapper.Map<IEnumerable<PassengerRouteDto>>(routesFromRepo);

            return new PagedList<PassengerRouteDto>(routesDtos.AsQueryable(), resourceParameters.PageNumber,resourceParameters.PageSize);
        }
    }
}

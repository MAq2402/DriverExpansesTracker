using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy = "User")]
    [ValidateAuthorizedUserFilter]
    [ValidateIfUserIsNotLoggedOut]
    public class PassengerRoutesController : BaseController
    {
        private IPassengerRouteService _passengerRouteService;
        private IUserService _userService;
        private IJourneyService _journeyService;

        public PassengerRoutesController(IPassengerRouteService passengerRouteService,
            IUserService userService,
            IJourneyService journeyService,
            IUrlHelper urlHelper,
            ILinksService linksService) : base(urlHelper, linksService)
        {
            _passengerRouteService = passengerRouteService;
            _userService = userService;
            _journeyService = journeyService;
        }

        [HttpGet("passengerRoutes", Name = Constants.RouteNames.GetPassengerRoutes)]

        public async Task<IActionResult> GetPassengerRoutes(string userId, int? journeyId, ResourceParameters resourceParameters)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var pagedRoutes = _passengerRouteService.GetPagedRoutes(userId, resourceParameters);

            AddPaginationHeader(pagedRoutes, Constants.RouteNames.GetPassengerRoutes, resourceParameters);

            var routesToReturn = pagedRoutes.ToList();

            await AddLinksToCollectionAsync(routesToReturn);

            return Ok(routesToReturn);
        }

        [HttpGet("journeys/{journeyId}/passengerRoutes", Name = Constants.RouteNames.GetPassengerRoutesByJourney)]
        public async Task<IActionResult> GetPassengerRoutesByJourney(string userId, int journeyId, ResourceParameters resourceParameters)
        {
            // has to check if user exists for both, because journey is not connected with this particular user

            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            if (!_journeyService.JourneyExists(journeyId))
            {
                return NotFound();
            }
            var pagedRoutes = _passengerRouteService.GetPagedRoutes(userId, journeyId, resourceParameters);

            AddPaginationHeader(pagedRoutes, Constants.RouteNames.GetPassengerRoutesByJourney, resourceParameters);

            var routesToReturn = pagedRoutes.ToList();

            await AddLinksToCollectionAsync(routesToReturn);

            return Ok(routesToReturn);
        }

        [HttpGet("passengerRoutes/{id}",Name =Constants.RouteNames.GetPassengerRoute)]

        public async Task<IActionResult> GetPassengerRoute(string userId, int id)
        {

            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var route = _passengerRouteService.GetRoute(userId, id);

            if (route == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(route);

            return Ok(route);
        }

        [HttpGet("journeys/{journeyId}/passengerRoutes/{id}", Name = Constants.RouteNames.GetPassengerRouteByJourney)]

        public async Task<IActionResult> GetPassengerRouteByJourney(string userId, int id, int journeyId)
        {
            // has to check if user exists for both, because journey is not connected with this particular user
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }


            if (!_journeyService.JourneyExists(journeyId))
            {
                return NotFound();
            }
            var route = _passengerRouteService.GetRoute(userId, journeyId, id);

            if (route == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(route);

            return Ok(route);
        }
    }
}

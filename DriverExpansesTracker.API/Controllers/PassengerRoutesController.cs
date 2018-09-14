using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy = "User")]
    [ValidateAuthorizedUserFilter]
    public class PassengerRoutesController : Controller
    {
        private IPassengerRouteService _passengerRouteService;
        private IUserService _userService;
        private IJourneyService _journeyService;

        public PassengerRoutesController(IPassengerRouteService passengerRouteService, IUserService userService,IJourneyService journeyService)
        {
            _passengerRouteService = passengerRouteService;
            _userService = userService;
            _journeyService = journeyService;
        }

        [HttpGet]
        [Route("passengerRoutes")]
        [Route("journeys/{journeyId}/passengerRoutes")]

        public IActionResult GetPassengerRoutes(string userId, int? journeyId)
        {
            if (journeyId != null)
            {
                if(!_journeyService.JourneyExists(userId,(int)journeyId))
                {
                    return NotFound();
                }
                var routes = _passengerRouteService.GetRoutes(userId, (int)journeyId);

                return Ok(routes);
            }
            else
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }
                var routes = _passengerRouteService.GetRoutes(userId);

                return Ok(routes);
            }
        }

        [HttpGet]
        [Route("passengerRoutes/{id}")]
        [Route("journeys/{journeyId}/passengerRoutes/{id}")]
        public IActionResult GetPassengerRoute(string userId, int id,  int? journeyId)
        {
            

            if (journeyId != null)
            {

                if (!_journeyService.JourneyExists(userId, (int)journeyId))
                {
                    return NotFound();
                }
                var route = _passengerRouteService.GetRoute(userId, (int)journeyId,id);

                if(route==null)
                {
                    return NotFound();
                }

                return Ok(route);
            }
            else
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }
                var route = _passengerRouteService.GetRoute(userId,id);

                if (route == null)
                {
                    return NotFound();
                }

                return Ok(route);
            }

        }
    }
}

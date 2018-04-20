using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    public class PassengerRoutesController : Controller
    {
        private IPassengerRouteService _passengerRouteService;
        private IUserService _userService;

        public PassengerRoutesController(IPassengerRouteService passengerRouteService, IUserService userService)
        {
            _passengerRouteService = passengerRouteService;
            _userService = userService;
        }

        [HttpGet]
        [Route("passengerRoutes")]
        [Route("journeys/{journeyId}/passengerRoutes")]

        public IActionResult GetPassengerRoutes(string userId, int? journeyId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            if (journeyId != null)
            {
                var routes = _passengerRouteService.GetRoutes(userId, (int)journeyId);

                return Ok(routes);
            }
            else
            {
                var routes = _passengerRouteService.GetRoutes(userId);

                return Ok(routes);
            }
        }

        [HttpGet]
        [Route("passengerRoutes/{id}")]
        [Route("journeys/{journeyId}/passengerRoutes/{id}")]
        public IActionResult GetPassengerRoute(string userId, int id,  int? journeyId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            if (journeyId != null)
            {
                var route = _passengerRouteService.GetRoute(userId, (int)journeyId,id);

                if(route==null)
                {
                    return NotFound();
                }

                return Ok(route);
            }
            else
            {
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

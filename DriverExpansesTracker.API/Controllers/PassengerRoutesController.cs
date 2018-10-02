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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy = "User")]
    [ValidateAuthorizedUserFilter]
    public class PassengerRoutesController : BaseController
    {
        private IPassengerRouteService _passengerRouteService;
        private IUserService _userService;
        private IJourneyService _journeyService;

        public PassengerRoutesController(IPassengerRouteService passengerRouteService, IUserService userService,IJourneyService journeyService, IUrlHelper urlHelper):base(urlHelper)
        {
            _passengerRouteService = passengerRouteService;
            _userService = userService;
            _journeyService = journeyService;
        }

        [HttpGet]
        [Route("passengerRoutes", Name = Constants.RouteNames.GetPassengerRoutes)]
        [Route("journeys/{journeyId}/passengerRoutes", Name = Constants.RouteNames.GetPassengerRoutesByJourney)]
        public IActionResult GetPassengerRoutes(string userId, int? journeyId,ResourceParameters resourceParameters)
        {
            // has to check if user exists for both, because journey is not connected with this particular user
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            if (journeyId.HasValue)
            {
                if(!_journeyService.JourneyExists(journeyId.Value))
                {
                    return NotFound();
                }
                var pagedRoutes = _passengerRouteService.GetPagedRoutes(userId, journeyId.Value, resourceParameters);

                pagedRoutes.Header.PreviousPageLink = pagedRoutes.HasPrevious ? CreateResourceUri(Constants.RouteNames.GetPassengerRoutesByJourney, resourceParameters, ResourceUriType.PreviousPage) : null;
                pagedRoutes.Header.NextPageLink = pagedRoutes.HasNext ? CreateResourceUri(Constants.RouteNames.GetPassengerRoutesByJourney, resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add(Constants.Headers.XPagination, pagedRoutes.Header.ToJson());

                return Ok(pagedRoutes.ToList());
            }
            else
            {
                var pagedRoutes = _passengerRouteService.GetPagedRoutes(userId,resourceParameters);

                pagedRoutes.Header.PreviousPageLink = pagedRoutes.HasPrevious ? CreateResourceUri(Constants.RouteNames.GetPassengerRoutes, resourceParameters, ResourceUriType.PreviousPage) : null;
                pagedRoutes.Header.NextPageLink = pagedRoutes.HasNext ? CreateResourceUri(Constants.RouteNames.GetPassengerRoutes, resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add(Constants.Headers.XPagination, pagedRoutes.Header.ToJson());

                return Ok(pagedRoutes.ToList());
            }
        }

        [HttpGet]
        [Route("passengerRoutes/{id}")]
        [Route("journeys/{journeyId}/passengerRoutes/{id}")]
        public IActionResult GetPassengerRoute(string userId, int id,  int? journeyId)
        {
            // has to check if user exists for both, because journey is not connected with this particular user
            if (!_userService.UserExists(userId)) 
            {
                return NotFound();
            }

            if (journeyId.HasValue)
            {

                if (!_journeyService.JourneyExists(journeyId.Value))
                {
                    return NotFound();
                }
                var route = _passengerRouteService.GetRoute(userId, journeyId.Value ,id);

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

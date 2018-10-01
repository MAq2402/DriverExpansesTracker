using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.Journey;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy = "User")]
    [ValidateAuthorizedUserFilter]
    public class JourneysController : BaseController
    {
        private IUserService _userService;
        private IJourneyService _journeyService;
        private ICarService _carService;
        private IPassengerRouteService _passengerRouteService;
        private IPaymentService _paymentService;


        public JourneysController(IUserService userService,
            IJourneyService journeyService,
            ICarService carService,
            IPassengerRouteService passengerRouteService,
            IPaymentService paymentService,
            IUrlHelper urlHelper):base(urlHelper)
        {
            _userService = userService;
            _journeyService = journeyService;
            _carService = carService;
            _passengerRouteService = passengerRouteService;
            _paymentService = paymentService;
        }
        [HttpGet]
        [Route("cars/{carId}/journeys", Name = Constants.RouteNames.GetJourneysByCar)]
        [Route("journeys",Name = Constants.RouteNames.GetJourneys)]


        public IActionResult GetJourneys(string userId, ResourceParameters resourceParameters,int? carId = null)
        {

            if (carId == null)
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }

                var pagedJourneys = _journeyService.GetPagedJourneys(userId,resourceParameters);

                pagedJourneys.Header.NextPageLink = pagedJourneys.HasPrevious ? CreateResourceUri(Constants.RouteNames.GetJourneys, resourceParameters, ResourceUriType.PreviousPage) : null;
                pagedJourneys.Header.NextPageLink = pagedJourneys.HasNext ? CreateResourceUri(Constants.RouteNames.GetJourneys, resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add("X-Pagination", pagedJourneys.Header.ToJson());

                return Ok(pagedJourneys.ToList());

            }
            else
            {
                if (!_carService.CarExists(userId, (int)carId,true))
                {
                    return NotFound();
                }

                var pagedJourneys = _journeyService.GetPagedJourneys(userId, resourceParameters, carId.Value);

                pagedJourneys.Header.NextPageLink = pagedJourneys.HasPrevious ? CreateResourceUri(Constants.RouteNames.GetJourneysByCar, resourceParameters, ResourceUriType.PreviousPage) : null;
                pagedJourneys.Header.NextPageLink = pagedJourneys.HasNext ? CreateResourceUri(Constants.RouteNames.GetJourneysByCar, resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add("X-Pagination", pagedJourneys.Header.ToJson());

                return Ok(pagedJourneys.ToList());
            }
        }
        [HttpGet]
        [Route("cars/{carId}/journeys/{id}", Name = Constants.RouteNames.GetJourney)]
        [Route("journeys/{id}")]
        public IActionResult GetJourney(string userId, int id, int? carId = null)
        {
            if (carId == null)
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }

                var journey = _journeyService.GetJourney(userId, id);

                if (journey == null)
                {
                    return NotFound();
                }

                return Ok(journey);
            }
            else
            {
                if (!_carService.CarExists(userId, (int)carId,true))
                {
                    return NotFound();
                }

                var journey = _journeyService.GetJourney(userId, (int)carId, id);


                if (journey == null)
                {
                    return NotFound();
                }

                return Ok(journey);
            }

        }
        [HttpPost, Route("cars/{carId}/journeys")]
        public IActionResult CreateJourney([FromBody]JourneyForCreationDto journeyFromBody, string userId,int carId)
        {
            var car = _carService.GetCar(userId, carId,true);

            if (car == null)
            {
                ModelState.AddModelError("", "Car with this id does not exist");
                return BadRequest(ModelState);
            }

            var routes = journeyFromBody.Routes;

            if (!_passengerRouteService.RoutesUsersExist(routes))
            {
                ModelState.AddModelError("", "User from route does not exist");
                return BadRequest(ModelState);
            }

            if (_passengerRouteService.SameUserForMultipleRoutes(routes))
            {
                ModelState.AddModelError("", "Multiple users for the same route");
                return BadRequest(ModelState);
            }          

            var journey = _journeyService.AddJourney(userId,journeyFromBody,car);

            _journeyService.SetTotalPrices(journey, car.FuelConsumption100km, journeyFromBody.PriceForLiter);

            var payments = _paymentService.AddPayments(journey);

            _userService.EditUsersPaymentStatistics(userId,payments);

            var journeyToReturn = _journeyService.GetJourney(journey);

            return CreatedAtRoute("GetJourney", new {  userId=userId, id = journey.Id, carId = journey.CarId  }, journeyToReturn);
        }
    }

}






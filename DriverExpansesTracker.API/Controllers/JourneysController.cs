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
using RiskFirst.Hateoas;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy = "User")]
    [ValidateAuthorizedUserFilter]
    [ValidateIfUserIsNotLoggedOut]
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
            IUrlHelper urlHelper,ILinksService linksService):base(urlHelper,linksService)
        {
            _userService = userService;
            _journeyService = journeyService;
            _carService = carService;
            _passengerRouteService = passengerRouteService;
            _paymentService = paymentService;
        }
        [HttpGet("journeys", Name = Constants.RouteNames.GetJourneys)]    
        public async Task<IActionResult> GetJourneys(string userId, ResourceParameters resourceParameters)
        {

            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var pagedJourneys = _journeyService.GetPagedJourneys(userId, resourceParameters);

            AddPaginationHeader(pagedJourneys, Constants.RouteNames.GetJourneys, resourceParameters);

            await AddLinksToCollectionAsync(pagedJourneys);

            return Ok(pagedJourneys);
        }

        [HttpGet("cars/{carId}/journeys", Name = Constants.RouteNames.GetJourneysByCar)]
        public async Task<IActionResult> GetJourneyByCar(string userId, ResourceParameters resourceParameters, int carId)
        {
            if (!_carService.CarExists(userId, carId, true))
            {
                return NotFound();
            }

            var pagedJourneys = _journeyService.GetPagedJourneys(userId, resourceParameters, carId);

            AddPaginationHeader(pagedJourneys, Constants.RouteNames.GetJourneysByCar, resourceParameters);

            await AddLinksToCollectionAsync(pagedJourneys);

            return Ok(pagedJourneys);
        }
        [HttpGet("journeys/{id}", Name = Constants.RouteNames.GetJourney)]
        public async Task<IActionResult> GetJourney(string userId, int id)
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
                await _linksService.AddLinksAsync(journey);
                return Ok(journey);
        }

        [HttpGet("cars/{carId}/journeys/{id}", Name = Constants.RouteNames.GetJourneyByCar)]
        public async Task<IActionResult> GetJourneyByCar(string userId, int id, int carId)
        {
            if (!_carService.CarExists(userId, (int)carId, true))
            {
                return NotFound();
            }

            var journey = _journeyService.GetJourney(userId, (int)carId, id);


            if (journey == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(journey);
            return Ok(journey);
        }
        [HttpPost("cars/{carId}/journeys", Name = Constants.RouteNames.CreateJourney)]
        [ValidateModelFilter]
        public async Task<IActionResult> CreateJourney([FromBody]JourneyForCreationDto journeyFromBody, string userId,int carId)
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

            await _linksService.AddLinksAsync(journeyToReturn);

            return CreatedAtRoute(Constants.RouteNames.GetJourneyByCar, new {  userId=userId, id = journey.Id, carId = journey.CarId  }, journeyToReturn);
        }

        [HttpDelete("journeys/{id}", Name =Constants.RouteNames.DeleteJourney)]

        public IActionResult DeleteJourney(string userId,int id)
        {
            if (!_journeyService.JourneyExists(userId, id))
            {
                return NotFound();
            }

            _journeyService.DeleteJourney(userId, id);

            return NoContent();
        }
    }

}






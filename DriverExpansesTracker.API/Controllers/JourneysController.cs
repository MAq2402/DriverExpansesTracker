using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.Journey;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
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
        [Route("cars/{carId}/journeys")]
        [Route("journeys",Name ="GetJourneys")]


        public IActionResult GetJourneys(string userId, JourneyResourceParameters resourceParameters,int? carId = null)
        {

            if (carId == null)
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }

                var journeys = _journeyService.GetJourneys(userId);

                return Ok(journeys);

            }
            else
            {
                if (!_carService.CarExists(userId, (int)carId))
                {
                    return NotFound();
                }

                //var pagedJourneys = 

                var journeys = _journeyService.GetJourneys(userId, (int)carId);

                return Ok(journeys);
            }
        }
        [HttpGet]
        [Route("cars/{carId}/journeys/{id}", Name = "GetJourney")]
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
                if (!_carService.CarExists(userId, (int)carId))
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
        [HttpPost, Route("journeys")]
        public IActionResult CreateJourney([FromBody]JourneyForCreationDto journeyFromBody, string userId)
        {
            var car = _carService.GetCar(userId, journeyFromBody.CarId);

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

            var journey = _journeyService.AddJourney(userId,journeyFromBody);

            _journeyService.SetTotalPrices(journey, car.FuelConsumption100km, journeyFromBody.PriceForLiter);

            var payments = _paymentService.AddPayments(journey);

            _userService.EditUsersPaymentStatistics(userId,payments);

            var journeyToReturn = _journeyService.GetJourney(journey);

            return CreatedAtRoute("GetJourney", new {  userId=userId, id = journey.Id, carId = journey.CarId  }, journeyToReturn);
        }
    }

}






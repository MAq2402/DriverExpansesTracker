using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Models.Journey;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    public class JourneysController : Controller
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
            IPaymentService paymentService)
        {
            _userService = userService;
            _journeyService = journeyService;
            _carService = carService;
            _passengerRouteService = passengerRouteService;
            _paymentService = paymentService;
        }
        [HttpGet]
        [Route("cars/{carId}/journeys")]
        [Route("journeys")]


        public IActionResult GetJourneys(string userId, int? carId = null)
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
            if (!_carService.CarExists(userId,journeyFromBody.CarId))
            {
                return BadRequest();
            }

            var routes = journeyFromBody.Routes;

            if (!_passengerRouteService.RoutesUsersExist(routes))
            {
                return BadRequest();
            }

            if (_passengerRouteService.SameUserForMultipleRoutes(routes))
            {
                return BadRequest();
            }          
            var journey = _journeyService.AddJourney(userId,journeyFromBody);

            var payments = _paymentService.AddPayments(userId,journey);

            _userService.EditUsersPaymentStatistics(userId,payments);

            return CreatedAtRoute("GetJourney", new { userId = userId, id = journey.Id, carId = journey.CarId  }, journey);
        }
    }

}






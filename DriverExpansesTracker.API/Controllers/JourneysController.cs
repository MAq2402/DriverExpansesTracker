using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public JourneysController(IUserService userService,IJourneyService journeyService)
        {
            _userService = userService;
            _journeyService = journeyService;
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

                //var journeys = _journeyService.GetJourneysForUser(userId);

                //return Ok(journeys);
                ;
            }
            //else
            //{
            //    if (!_carRepository.CarExistsForUser(userId, (int)carId))
            //    {
            //        return NotFound();
            //    }

            //    var journeys = _journeyRepository.GetJourneysForUserAndCar(userId, (int)carId);

            //    return Ok(journeys);
            //}
            return Ok();
        }

    }
}

        //    [HttpGet]
        //    [Route("cars/{carId}/journeys/{id}", Name = "GetJourneyForUserAndCar")]
        //    [Route("journeys/{id}")]
        //    public IHttpActionResult GetJourney(int userId, int id, bool passangerRoutes, int? carId = null)
        //    {
        //        try
        //        {

        //            if (carId == null)
        //            {
        //                if (!_userRepository.UserExists(userId))
        //                {
        //                    return NotFound();
        //                }

        //                var journey = _journeyRepository.GetJourneyForUser(userId, id, passangerRoutes);

        //                if (journey == null)
        //                {
        //                    return NotFound();
        //                }

        //                Mapper.Map<JourneyDto>(journey);

        //                return Ok(journey);
        //            }
        //            else
        //            {
        //                if (!_carRepository.CarExistsForUser(userId, (int)carId))
        //                {
        //                    return NotFound();
        //                }

        //                var journey = _journeyRepository.GetJourneyForUserAndCar(userId, (int)carId, id, passangerRoutes);

        //                Mapper.Map<JourneyDto>(journey);

        //                if (journey == null)
        //                {
        //                    return NotFound();
        //                }

        //                return Ok(journey);
        //            }

        //        }
        //        catch (Exception)
        //        {
        //            return InternalServerError();
        //        }

        //    }

        //    [HttpPost, Route("cars/{carId}/journeys")]
        //    public IHttpActionResult CreateJourney([FromBody]JourneyForCreationDto JourneyFromBody, int userId, int carId)
        //    {
        //        try
        //        {
        //            if (JourneyFromBody == null)
        //            {
        //                return BadRequest();
        //            }

        //            var car = _carRepository.GetCarForUser(userId, carId, false);

        //            if (car == null)
        //            {
        //                return NotFound();
        //            }

        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            var routes = Mapper.Map<IEnumerable<PassengerRoute>>(JourneyFromBody.Routes);

        //            if (!_passengerRouteRepository.UsersExistForRoutes(routes))
        //            {
        //                return BadRequest();
        //            }

        //            if (_passengerRouteService.SameUserForRoutes(routes))
        //            {
        //                return BadRequest();
        //            }

        //            _passengerRouteRepository.SetUsersForRoutes(routes);

        //            var journey = Mapper.Map<Journey>(JourneyFromBody);

        //            journey.PassengerRoutes.AddRange(routes);

        //            _journeyService.GiveTotalPrices(journey, (double)JourneyFromBody.PriceForLiter, car.FuelConsumption100km);

        //            var payments = _paymentService.GeneratePayments(journey, userId);

        //            _userService.EditUsersPaymentStatistics(payments, userId);

        //            //notification service albo repozytorium wysle notyfikacje 

        //            _journeyRepository.AddJourneyForUserAndCar(userId, carId, journey);

        //            _paymentRepository.AddPayments(payments);

        //            if (!_appRepository.Commit())
        //            {
        //                return InternalServerError();
        //            }

        //            var journeyToReturn = Mapper.Map<JourneyDto>(journey);

        //            return CreatedAtRoute("GetJourneyForUserAndCar", new { userId = userId, id = journey.Id, passengerRoutes = true, carId = carId }, journeyToReturn);

        //        }
        //        catch (Exception e)
        //        {
        //            return InternalServerError();
        //        }

        //    }
        //}
        
    

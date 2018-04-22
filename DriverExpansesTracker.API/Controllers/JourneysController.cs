﻿using System;
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

        public JourneysController(IUserService userService, IJourneyService journeyService, ICarService carService)
        {
            _userService = userService;
            _journeyService = journeyService;
            _carService = carService;
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
        [Route("cars/{carId}/journeys/{id}", Name = "GetJourneyForUserAndCar")]
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
        //[HttpPost, Route("journeys")]
        //public IActionResult CreateJourney([FromBody]JourneyForCreationDto journeyFromBody, int userId)
        //{
        //    //czu car istnieje

        //    var routes = Mapper.Map<IEnumerable<PassengerRoute>>(journeyFromBody.Routes);

        //    if (!_passengerRouteRepository.UsersExistForRoutes(routes))
        //    {
        //        return BadRequest();
        //    }

        //    if (_passengerRouteService.SameUserForRoutes(routes))
        //    {
        //        return BadRequest();
        //    }

        //    _passengerRouteRepository.SetUsersForRoutes(routes);

        //    var journey = Mapper.Map<Journey>(journeyFromBody);

        //    journey.PassengerRoutes.AddRange(routes);

        //    _journeyService.GiveTotalPrices(journey, (double)journeyFromBody.PriceForLiter, car.FuelConsumption100km);

        //    var payments = _paymentService.GeneratePayments(journey, userId);

        //    _userService.EditUsersPaymentStatistics(payments, userId);

        //    //notification service albo repozytorium wysle notyfikacje

        //    _journeyRepository.AddJourneyForUserAndCar(userId, carId, journey);

        //    _paymentRepository.AddPayments(payments);

        //    if (!_appRepository.Commit())
        //    {
        //        return InternalServerError();
        //    }

        //    var journeyToReturn = Mapper.Map<JourneyDto>(journey);

        //    return CreatedAtRoute("GetJourneyForUserAndCar", new { userId = userId, id = journey.Id, passengerRoutes = true, carId = carId }, journeyToReturn);

        //}
    }

}






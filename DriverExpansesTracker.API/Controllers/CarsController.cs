using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Services.Models.Car;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}/cars")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy ="User")]
    //[Authorize]
    //[ServiceFilter(typeof(ValidateIfUserExists))]
    public class CarsController : Controller
    {
        private ICarService _carService;
        private IUserService _userService;

        public CarsController(ICarService carService,IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }

        [HttpGet()]
        public IActionResult GetCars(string userId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var cars = _carService.GetCars(userId);

            return Ok(cars);
        }

        [HttpGet("{id}",Name ="GetCar")]
        public IActionResult GetCar(string userId, int id)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var car = _carService.GetCar(userId, id);

            if(car==null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost()]
        [ValidateModelFilter]
        public IActionResult CreateCar([FromBody] CarForCreationDto carFromBody,string userId)
        {
            if(!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var carToRetrun = _carService.AddCar(carFromBody, userId);

            return CreatedAtRoute("GetCar", new { userId = userId, id = carToRetrun.Id }, carToRetrun);


        }
    }
}

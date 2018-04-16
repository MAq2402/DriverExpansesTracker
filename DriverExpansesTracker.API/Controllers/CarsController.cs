using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Mvc;


namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}/cars")]
    public class CarsController : Controller
    {
        private IAppService _appService;
        private ICarService _carService;
        private IUserService _userService;

        public CarsController(IAppService appService,ICarService carService,IUserService userService)
        {
            _appService = appService;
            _carService = carService;
            _userService = userService;
        }

       //[HttpGet()]
       //public IActionResult GetCars(string userId)
       // {
       //     if(!_userService.UserExists())

       //     var cars = _carService.GetCarsForUser(userId);

       //     return Ok();
       // }

       // [HttpGet("{id}")]
       // public IActionResult GetCar(string userName,int id)
       // {

       // }
    }
}

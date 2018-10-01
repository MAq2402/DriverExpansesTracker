using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.API.Helpers;
using DriverExpansesTracker.Services.Models.Car;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}/cars")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy ="User")]
    [ValidateAuthorizedUserFilter]
    public class CarsController : BaseController
    {
        private ICarService _carService;
        private IUserService _userService;

        public CarsController(ICarService carService, IUserService userService, IUrlHelper urlHelper):base(urlHelper)
        {
            _carService = carService;
            _userService = userService;
        }

        [HttpGet()]
        public IActionResult GetCars(string userId, bool onlyActive = true)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var cars = _carService.GetCars(userId, onlyActive);

            return Ok(cars);
        }

        [HttpGet("{id}", Name = Constants.RouteNames.GetCar)]
        public IActionResult GetCar(string userId, int id, bool onlyActive = true)
        {
            var car = _carService.GetCar(userId, id, onlyActive);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }



        [HttpPost()]
        [ValidateModelFilter]
        public IActionResult CreateCar([FromBody] CarForCreationDto carFromBody, string userId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var carToRetrun = _carService.AddCar(carFromBody, userId);

            return CreatedAtRoute(Constants.RouteNames.GetCar, new { userId = userId, id = carToRetrun.Id }, carToRetrun);
        }

        [HttpDelete("{id}")]

        public IActionResult ChangeStatusToInactive(string userId, int id)
        {

            if (!_carService.CarExists(userId, id,true))
            {
                return NotFound();
            }

            _carService.ChangeToInactive(userId, id);

            return NoContent();
        }

        [HttpPatch("{id}")]

        public IActionResult PartiallyUpdateCar(string userId, int id,[FromBody] JsonPatchDocument<CarForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var carFromRepo = _carService.GetCarEntity(userId, id);

            if (carFromRepo == null)
            {
                return NotFound();
            }

            var carForUpdate = _carService.GetCarForUpdate(carFromRepo);

            patchDoc.ApplyTo(carForUpdate,ModelState);

            TryValidateModel(carForUpdate);

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectresult(ModelState);
            }

            _carService.UpdateCar(carFromRepo,carForUpdate);
            
            return NoContent();
        }
    }
}

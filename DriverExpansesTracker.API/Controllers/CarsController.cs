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
using RiskFirst.Hateoas;

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

        public CarsController(ICarService carService, IUserService userService,ILinksService linksService, IUrlHelper urlHelper):base(urlHelper,linksService)
        {
            _carService = carService;
            _userService = userService;
        }

        [HttpGet(Name = Constants.RouteNames.GetCars)]
        public async Task<IActionResult> GetCars(string userId, bool onlyActive = true)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var cars = _carService.GetCars(userId, onlyActive);

            await AddLinksToCollectionAsync(cars);

            return Ok(cars);
        }

      

        [HttpGet("{id}", Name = Constants.RouteNames.GetCar)]
        public async Task<IActionResult> GetCar(string userId, int id, bool onlyActive = true)
        {
            var car = _carService.GetCar(userId, id, onlyActive);

            if (car == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(car);
            
            return Ok(car);
        }



        [HttpPost(Name = Constants.RouteNames.CreateCar)]
        [ValidateModelFilter]
        public async Task<IActionResult> CreateCar([FromBody] CarForCreationDto carFromBody, string userId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }

            var carToRetrun = _carService.AddCar(carFromBody, userId);

            await _linksService.AddLinksAsync(carToRetrun);

            return CreatedAtRoute(Constants.RouteNames.GetCar, new { userId = userId, id = carToRetrun.Id }, carToRetrun);
        }

        [HttpDelete("{id}", Name =Constants.RouteNames.ChangeStatusToInactive)]

        public IActionResult ChangeStatusToInactive(string userId, int id)
        {

            if (!_carService.CarExists(userId, id,true))
            {
                return NotFound();
            }

            _carService.ChangeToInactive(userId, id);

            return NoContent();
        }

        [HttpPatch("{id}",Name = Constants.RouteNames.PartiallyUpdateCar)]

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

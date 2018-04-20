using DriverExpansesTracker.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface ICarService
    {
        CarDto GetCar(string userId, int id);
        IEnumerable<CarDto> GetCars(string userId);
        CarDto AddCar(CarForCreationDto car, string userId);
        bool CarExists(string userId, int carId);
    }
}

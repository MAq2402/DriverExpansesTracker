using DriverExpansesTracker.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface ICarService
    {
        CarDto GetCarForUser(string userId, int id);
        IEnumerable<CarDto> GetCarsForUser(string userId);
        CarDto AddCarForUser(CarForCreationDto car, string userId);
    }
}

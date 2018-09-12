using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface ICarService
    {
        CarDto GetCar(string userId, int id,bool onlyActive);
        IEnumerable<CarDto> GetCars(string userId, bool onlyActive);

        CarDto AddCar(CarForCreationDto car, string userId);
        bool CarExists(string userId, int carId);

        bool ActiveCarExists(string userId, int carId);

        void ChangeToInactive(string userId, int id);
    }
}

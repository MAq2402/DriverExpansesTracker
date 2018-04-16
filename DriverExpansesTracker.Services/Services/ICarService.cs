using DriverExpansesTracker.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface ICarService
    {
        IEnumerable<CarDto> GetCarsForUser(string userId);
    }
}

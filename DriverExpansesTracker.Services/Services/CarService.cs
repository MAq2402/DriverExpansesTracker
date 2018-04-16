using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Models.Car;

namespace DriverExpansesTracker.Services.Services
{
    public class CarService : ICarService
    {
        private IRepository<Car> _carRepository;

        public CarService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public IEnumerable<CarDto> GetCarsForUser(string userId)
        {
            var cars = _carRepository.FindBy(c=>c.UserId == userId);

            return Mapper.Map<IEnumerable<CarDto>>(cars);
        }
    }
}

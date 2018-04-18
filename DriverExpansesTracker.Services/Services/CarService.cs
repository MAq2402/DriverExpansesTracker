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

        public CarDto AddCarForUser(CarForCreationDto car, string userId)
        {
            var carToSave = Mapper.Map<Car>(car);
            carToSave.UserId = userId;

            _carRepository.Add(carToSave);

            if(!_carRepository.Save())
            {
                throw new Exception("Could not save car");
            }

            return Mapper.Map<CarDto>(carToSave);
        }

        public CarDto GetCarForUser(string userId, int id)
        {
            var car = _carRepository.FindSingleBy(c => c.Id == id && c.UserId == userId);

            return Mapper.Map<CarDto>(car);
        }

        public IEnumerable<CarDto> GetCarsForUser(string userId)
        {
            var cars = _carRepository.FindBy(c=>c.UserId == userId);

            return Mapper.Map<IEnumerable<CarDto>>(cars);
        }
    }
}

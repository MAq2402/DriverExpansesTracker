using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool ActiveCarExists(string userId, int carId)
        {
            if (_carRepository.FindBy(c => c.UserId == userId && c.Id == carId&&c.Active).Any())
            {
                return true;
            }
            return false;
        }

        public CarDto AddCar(CarForCreationDto car, string userId)
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

        public bool CarExists(string userId, int carId)
        {
            if(_carRepository.FindBy(c=>c.UserId==userId&&c.Id==carId).Any())
            {
                return true;
            }
            return false;
        }

        public void ChangeToInactive(string userId, int id)
        {
            var car = _carRepository.FindSingleBy(x => x.UserId == userId && x.Id == id && x.Active);

            car.Active = false;

            if (!_carRepository.Save())
            {
                throw new Exception("Could not disactivate car");
            }
        }


        public CarDto GetCar(string userId, int id,bool onlyActive)
        {
            if (onlyActive)
            {
                var car = _carRepository.FindSingleBy(c => c.Id == id && c.UserId == userId&&c.Active);
                return Mapper.Map<CarDto>(car);
            }
            else
            {
                var car = _carRepository.FindSingleBy(c => c.Id == id && c.UserId == userId);
                return Mapper.Map<CarDto>(car);
            }
            

            
        }

        public IEnumerable<CarDto> GetCars(string userId,bool onlyActive)
        {

            if (onlyActive)
            {
                var cars = _carRepository.FindBy(c => c.UserId == userId && c.Active);

                return Mapper.Map<IEnumerable<CarDto>>(cars);
            }
            else
            {
                var cars = _carRepository.FindBy(c => c.UserId == userId);

                return Mapper.Map<IEnumerable<CarDto>>(cars);
            }
            
        }
    }
}

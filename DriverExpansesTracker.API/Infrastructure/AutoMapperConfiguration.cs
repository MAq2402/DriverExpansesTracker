using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.User, DriverExpansesTracker.Services.Models.User.UserDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.User.UserForCreationDto, DriverExpansesTracker.Repository.Entities.User>()
                   .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Car, DriverExpansesTracker.Services.Models.Car.CarDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.Car.CarForCreationDto, DriverExpansesTracker.Repository.Entities.Car>();
                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Car, DriverExpansesTracker.Services.Models.Car.CarForUpdateDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.Car.CarForUpdateDto, DriverExpansesTracker.Repository.Entities.Car>();

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Journey, DriverExpansesTracker.Services.Models.Journey.JourneyDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.Journey.JourneyForCreationDto, DriverExpansesTracker.Repository.Entities.Journey>()
                   .ForMember(dest => dest.PassengerRoutes, opt => opt.MapFrom(src => src.Routes));

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.PassengerRoute, DriverExpansesTracker.Services.Models.PassengerRoute.PassengerRouteDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.PassengerRoute.PassengerRouteForCreationDto, DriverExpansesTracker.Repository.Entities.PassengerRoute>();

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Payment, DriverExpansesTracker.Services.Models.Payment.PaymentDto>();
            });
        }
    }
      
}

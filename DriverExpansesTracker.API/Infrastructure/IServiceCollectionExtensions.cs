using DriverExpansesTracker.Services.Models.Car;
using DriverExpansesTracker.Services.Models.Journey;
using DriverExpansesTracker.Services.Models.PassengerRoute;
using DriverExpansesTracker.Services.Models.Payment;
using DriverExpansesTracker.Services.Models.User;
using Microsoft.Extensions.DependencyInjection;
using RiskFirst.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static void AddHateoas(this IServiceCollection services)
        {
            services.AddLinks(config =>
            {

                config.AddPolicy<UserDto>(policy =>
                {
                    policy.RequireRoutedLink("get-by-id", Constants.RouteNames.GetUserById, x => new { id = x.Id })
                          .RequireRoutedLink("get-by-name", Constants.RouteNames.GetUserByName, x => new { userName = x.UserName })
                          .RequireRoutedLink("all", Constants.RouteNames.GetUsers)
                          .RequireRoutedLink("create", Constants.RouteNames.CreateUser);
                });

                config.AddPolicy<CarDto>(policy =>
                {
                    policy.RequireRoutedLink("get", Constants.RouteNames.GetCar, x => new { id = x.Id })
                          .RequireRoutedLink("all", Constants.RouteNames.GetCars)
                          .RequireRoutedLink("create", Constants.RouteNames.CreateCar)
                          .RequireRoutedLink("patch", Constants.RouteNames.PartiallyUpdateCar, x => new { id = x.Id })
                          .RequireRoutedLink("delete", Constants.RouteNames.ChangeStatusToInactive, x => new { id = x.Id });
                });

                config.AddPolicy<JourneyDto>(policy =>
                {

                    policy.RequireRoutedLink("get", Constants.RouteNames.GetJourney, x => new { id = x.Id })
                          .RequireRoutedLink("all", Constants.RouteNames.GetJourneys)
                          .RequireRoutedLink("create", Constants.RouteNames.CreateJourney, x => new { carId = x.CarId })
                          .RequireRoutedLink("delete", Constants.RouteNames.DeleteJourney, x => new { id = x.Id })
                          .RequireRoutedLink("all-by-car", Constants.RouteNames.GetJourneysByCar, x => new { carId = x.CarId})
                          .RequireRoutedLink("get-by-car", Constants.RouteNames.GetJourneyByCar, x => new {carId = x.CarId, id = x.Id });
                });

                config.AddPolicy<PaymentDto>(policy =>
                {

                    policy.RequireRoutedLink("get", Constants.RouteNames.GetPayment, x => new { id = x.Id })
                          .RequireRoutedLink("all", Constants.RouteNames.GetPayments)
                          .RequireRoutedLink("all-by-journey", Constants.RouteNames.GetPaymentsByJourney, x => new { journeyId = x.JourneyId })
                          .RequireRoutedLink("get-by-joruney", Constants.RouteNames.GetPaymentByJourney, x => new { journeyId = x.JourneyId, id = x.Id });
                });

                config.AddPolicy<PassengerRouteDto>(policy =>
                {

                    policy.RequireRoutedLink("get", Constants.RouteNames.GetPassengerRoute, x => new { id = x.Id })
                          .RequireRoutedLink("all", Constants.RouteNames.GetPassengerRoutes)
                          .RequireRoutedLink("all-by-journey", Constants.RouteNames.GetPassengerRoutesByJourney, x => new { journeyId = x.JourneyId })
                          .RequireRoutedLink("get-by-joruney", Constants.RouteNames.GetPassengerRouteByJourney, x => new { journeyId = x.JourneyId , id = x.Id });
                });
            });
        }
    }
}

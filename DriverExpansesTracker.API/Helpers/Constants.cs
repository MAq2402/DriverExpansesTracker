using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API
{
    public static class Constants
    {
        public static class JWT
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }
            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        public static class RouteNames
        {
            public const string GetJourneysByCar = "GetJourneysByCar";
            public const string GetJourneys = "GetJourneys";
            public const string GetCar = "GetCar";
            public const string GetUserByName = "GetUserByName";
            public const string GetUserById = "GetUserById";
            public const string GetJourney = "GetJourney";
            public const string GetPayments = "GetPayments";
            public const string GetPaymentsByJourney = "GetPaymentsByJourney";
            public const string GetPassengerRoutes = "GetPassengerRoutes";
            public const string GetPassengerRoutesByJourney = "GetPassengerRoutesByJourney";
            public const string GetCars = "GetCars";
            public const string CreateCar = "CreateCar";
            public const string ChangeStatusToInactive = "ChangeStatusToInactive";
            public const string PartiallyUpdateCar = "PartiallyUpdateCar";
            public const string CreateJourney = "CreateJourney";
            public const string DeleteJourney = "DeleteJourney";
            public const string GetJourneyByCar = "GetJourneyByCar";
            public const string GetUsers = "GetUsers";
            public const string GetPassengerRouteByJourney = "GetPassengerRouteByJourney";
            public const string GetPassengerRoute = "GetPassengerRoute";
            public const string CreateUser = "CreateUser";
            public const string GetPaymentByJourney = "GetPaymentByJourney";
            public const string GetPayment = "GetPayment";
        }

        public static class Headers
        {
            public const string XPagination = "X-Pagination";
        }
    }
}

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
        }

        public static class Headers
        {
            public const string XPagination = "X-Pagination";
        }
    }
}

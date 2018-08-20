using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity claimsIdentity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}

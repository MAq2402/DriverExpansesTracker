using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface IAuthService
    {
        void AddToken(string value);

        bool TokenExists(string value);
    }
}

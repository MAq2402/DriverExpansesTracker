using DriverExpansesTracker.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Tests.Fixtures
{
    public class MapperFixture : IDisposable
    {
        public MapperFixture()
        {
            AutoMapperConfiguration.Configure();
        }
        public void Dispose()
        {
            
        }
    }
}

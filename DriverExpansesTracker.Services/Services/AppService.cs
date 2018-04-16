using DriverExpansesTracker.Repository.Entities.Base;
using DriverExpansesTracker.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public class AppService : IAppService
    {
        private IRepository<Entity> _repository;

        public AppService(IRepository<Entity> repository)
        {
            _repository = repository;
        }
        public bool Save()
        {

            return _repository.Save();
        }
    }
}

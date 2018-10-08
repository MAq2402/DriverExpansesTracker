using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public class AuthService : IAuthService
    {
        private IRepository<ExpiredToken> _repository;

        public AuthService(IRepository<ExpiredToken> repository)
        {
            _repository = repository; 
        }
        public void AddToken(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }
            var expiredToken = new ExpiredToken(value);

            _repository.Add(expiredToken);

            if (!_repository.Save())
            {
                throw new Exception("Could not save token");
            }

        }

        public bool TokenExists(string value)
        {
            return _repository.FindSingleBy(et => et.Value == value) != null;
        }
    }
}

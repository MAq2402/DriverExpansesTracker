using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DriverExpansesTracker.Services.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByName(string name);
        bool UserExists(string name);
        void RemoveUser(string name);
    }
}

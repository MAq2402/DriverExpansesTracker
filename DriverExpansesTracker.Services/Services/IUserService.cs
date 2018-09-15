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
        UserDto GetUserById(string id);
        bool UserExists(string id);
        void EditUsersPaymentStatistics(string receiverId, IEnumerable<Payment> payments);
        User GetUserEntity(UserForCreationDto user);
        UserDto GetUser(User user);
    }
}

using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverExpansesTracker.Services.Services
{
    public class UserService :IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto GetUserByName(string name)
        {
            var user = _userRepository.FindBy(u => u.UserName == name).FirstOrDefault();
            return Mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _userRepository.GetAll();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public void RemoveUser(string name)
        {
            var user = _userRepository.FindSingleBy(u => u.UserName == name);
            _userRepository.Delete(user);
        }


        public bool UserExists(string name)
        {
            if(_userRepository.FindBy(u=>u.UserName==name).Any())
            {
                return true;
            }
            return false;
        }
    }
}

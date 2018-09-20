using AutoMapper;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Helpers;
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

        public UserDto GetUserById(string id)
        {
            var user = _userRepository.FindBy(u => u.Id == id).FirstOrDefault();
            return Mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByName(string name)
        {
            var user = _userRepository.FindBy(u => u.UserName == name).FirstOrDefault();
            return Mapper.Map<UserDto>(user);
        }



        public void EditUsersPaymentStatistics(string receiverId, IEnumerable<Payment> payments)
        {
            var receiver = _userRepository.FindSingleBy(u=>u.Id==receiverId);

            if (receiver == null)
            {
                throw new ArgumentNullException("Receiver does not exist");
            }

            receiver.ToReceive = payments.Sum(p => p.Amount);

            foreach (var payment in payments)
            {
                var payer = _userRepository.FindSingleBy(u=>u.Id==payment.PayerId);
                if (payer == null)
                {
                    throw new ArgumentNullException("Payer does not exist");
                }
                payer.ToPay = payment.Amount;
            }
        }


        public bool UserExists(string id)
        {
            return _userRepository.FindBy(u => u.Id == id).Any();
        }

        public User GetUserEntity(UserForCreationDto user)
        {
            return Mapper.Map<User>(user);
        }

        public UserDto GetUser(User user)
        {
            return Mapper.Map<UserDto>(user);
        }

        public PagedList<UserDto> GetPagedUsers(ResourceParameters resourceParameters)
        {
            var usersFromRepo = _userRepository.GetAll();

            var users = Mapper.Map<IEnumerable<UserDto>>(usersFromRepo);

            return new PagedList<UserDto>(users.AsQueryable(), resourceParameters.PageNumber, resourceParameters.PageSize);
        }
    }

    }

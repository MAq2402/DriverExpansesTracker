using AutoMapper;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users")]
    [ValidateModel]
    public class UsersController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users;

            var usersToReturn = Mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersToReturn);
        }
        [HttpGet("{userName}",Name ="GetUserByName")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user =  await _userManager.FindByNameAsync(userName);

            if(user==null)
            {
                return NotFound();
            }

            var userToReturn = Mapper.Map<UserDto>(user);

            return Ok(userToReturn);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto userFromBody)
        {
            var userToSave = Mapper.Map<User>(userFromBody);

            var result = await _userManager.CreateAsync(userToSave, userFromBody.Password);

            if(!result.Succeeded)
            {
                return BadRequest();
            }

            var userToReturn = Mapper.Map<UserDto>(userToSave);

            return CreatedAtRoute("GetUserByName",new { userName = userToReturn.UserName },userToReturn);
        }
        [HttpGet("currentIdentity")]
        public async Task<IActionResult> GetCurrentIdentity()
        {
            if(String.IsNullOrEmpty(User.Identity.Name))
            {
                return NotFound();
            }
            var currentIdentity = await _userManager.FindByNameAsync(User.Identity.Name);

            if(currentIdentity==null)
            {
                return NotFound();
            }

            var currentIdentityToReturn = Mapper.Map<UserDto>(currentIdentity);

            return Ok(currentIdentityToReturn);
        }
    }
}

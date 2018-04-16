using AutoMapper;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.User;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users")]
    [ValidateModelFilter]
    public class UsersController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IUserService _userService;
        private IAppService _appService;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager,IUserService userService,IAppService appService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _appService = appService;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
        
            return Ok(users);
        }
        [HttpGet("{userName}",Name ="GetUserByName")]
        public IActionResult GetUserByName(string userName)
        {
            var user = _userService.GetUserByName(userName);

            if(user==null)
            {
                return NotFound();
            }

            return Ok(user);
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto credentials)
        {

            if(credentials==null)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, false, false);

            if(!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();

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
        [HttpDelete("{userName}")]
        public IActionResult RemoveUser(string userName)
        {
            var user = _userService.GetUserByName(userName);

            if(user==null)
            {
                return NotFound();
            }

            _userService.RemoveUser(userName);

            if(!_appService.Save())
            {
                throw new Exception("Could not save to Db");
            }

            return NoContent();
        }
    }
}

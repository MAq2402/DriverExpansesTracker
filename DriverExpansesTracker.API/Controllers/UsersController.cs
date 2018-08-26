using AutoMapper;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.User;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users")]
    [EnableCors("MyPolicy")]

    public class UsersController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager,IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
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

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById(string id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost()]
        [ValidateModelFilter]
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
        [ValidateModelFilter]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto credentials)
        {

            var result = await _signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, false, false);

            if(!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();

        }
        [HttpPost("currentIdentity")]
        public  IActionResult GetCurrentIdentity([FromBody] Services.Models.Auth.Token token)
        {
            var tokenS = new JwtSecurityTokenHandler().ReadToken(token.Value) as JwtSecurityToken;
            var sub = tokenS?.Claims.First(claim => claim.Type == "sub")?.Value;
            
            var user = _userService.GetUserByName(sub);

            if(user==null)
            {
                return NoContent();
            }

            return Ok(user);
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

            return NoContent();
        }
    }
}

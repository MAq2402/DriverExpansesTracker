using AutoMapper;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.User;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "User")]

    public class UsersController:Controller
    {
        private UserManager<User> _userManager;
        private IUserService _userService;

        public UsersController(UserManager<User> userManager,IUserService userService)
        {
            _userManager = userManager;
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
        [AllowAnonymous]
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

        [HttpPost("currentIdentity")]
        public IActionResult GetCurrentIdentity([FromBody] Services.Models.Auth.Token token)
        {
            var tokenS = new JwtSecurityTokenHandler().ReadToken(token.Value) as JwtSecurityToken;
            var sub = tokenS?.Claims.First(claim => claim.Type == "sub")?.Value;

            var user = _userService.GetUserByName(sub);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        [HttpGet("currentIdentity")]
        public IActionResult GetCurrentIdentity()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            

            var user = string.IsNullOrEmpty(userId) ? null : _userService.GetUserById(userId);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }
    }
}

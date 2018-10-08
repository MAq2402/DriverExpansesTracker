using AutoMapper;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Models.User;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
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
    

    public class UsersController : BaseController
    {
        private UserManager<User> _userManager;
        private IUserService _userService;

        public UsersController(UserManager<User> userManager, IUserService userService, IUrlHelper urlHelper, ILinksService linksService) : base(urlHelper,linksService)
        {
            _userManager = userManager;
            _userService = userService;
        }
        [HttpGet(Name =Constants.RouteNames.GetUsers)]
        [ValidateIfUserIsNotLoggedOut]
        public async Task<IActionResult> GetUsers(ResourceParameters resourceParameters)
        {
            var pagedUsers = _userService.GetPagedUsers(resourceParameters);

            AddPaginationHeader(pagedUsers, Constants.RouteNames.GetUsers, resourceParameters);

            await AddLinksToCollectionAsync(pagedUsers);

            return Ok(pagedUsers);
        }
        [HttpGet("byName/{userName}", Name = Constants.RouteNames.GetUserByName)]
        [ValidateIfUserIsNotLoggedOut]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user = _userService.GetUserByName(userName);

            if (user == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(user);
            
            return Ok(user);
        }

        [HttpGet("{id}", Name = Constants.RouteNames.GetUserById)]
        [ValidateIfUserIsNotLoggedOut]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(user);

            return Ok(user);
        }

        [HttpPost()]
        [ValidateModelFilter]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto userFromBody)
        {
            var userToSave = _userService.GetUserEntity(userFromBody);

            var result = await _userManager.CreateAsync(userToSave, userFromBody.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var userToReturn = _userService.GetUser(userToSave);

            await _linksService.AddLinksAsync(userToReturn);

            return CreatedAtRoute(Constants.RouteNames.GetUserByName, new { userName = userToReturn.UserName }, userToReturn);
        }

        [HttpGet("currentIdentity")]
        [ValidateIfUserIsNotLoggedOut]
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

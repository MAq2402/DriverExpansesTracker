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

        public UsersController(UserManager<User> userManager, IUserService userService, IUrlHelper urlHelper) : base(urlHelper)
        {
            _userManager = userManager;
            _userService = userService;
        }
        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(ResourceParameters resourceParameters)
        {
            var pagedUsers = _userService.GetPagedUsers(resourceParameters);

            pagedUsers.Header.PreviousPageLink = pagedUsers.HasPrevious ? CreateResourceUri(nameof(GetUsers), resourceParameters, ResourceUriType.PreviousPage) : null;
            pagedUsers.Header.NextPageLink = pagedUsers.HasNext ? CreateResourceUri(nameof(GetUsers), resourceParameters, ResourceUriType.NextPage) : null;

            Response.Headers.Add(Constants.Headers.XPagination, pagedUsers.Header.ToJson());

            return Ok(pagedUsers);
        }
        [HttpGet("byName/{userName}", Name = Constants.RouteNames.GetUserByName)]
        public IActionResult GetUserByName(string userName)
        {
            var user = _userService.GetUserByName(userName);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{id}", Name = Constants.RouteNames.GetUserById)]
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
            var userToSave = _userService.GetUserEntity(userFromBody);

            var result = await _userManager.CreateAsync(userToSave, userFromBody.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var userToReturn = _userService.GetUser(userToSave);

            return CreatedAtRoute(Constants.RouteNames.GetUserByName, new { userName = userToReturn.UserName }, userToReturn);
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

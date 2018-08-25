using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Auth;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.API.Helpers;
using DriverExpansesTracker.API.Infrastructure;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Models.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/auth")]
    [EnableCors("MyPolicy")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }
        [HttpPost("login")]
        [ValidateModelFilter]
        public async Task<IActionResult> Post([FromBody]LoginDto credentials)
        {
            var identity = await GetClaimsIdentityAsync(credentials.UserName, credentials.Password);

            if(identity==null)
            {
                return BadRequest("Wrong credentials");
            }
            //var x = new Claim()

            var jwt =  await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return Ok(jwt);
        }
        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(string userName,string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);      
            }
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if(userToVerify==null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            if(await _userManager.CheckPasswordAsync(userToVerify,password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}

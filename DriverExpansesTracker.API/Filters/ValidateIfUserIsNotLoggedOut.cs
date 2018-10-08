using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using DriverExpansesTracker.API.Helpers;

namespace DriverExpansesTracker.API.Filters
{
    public class ValidateIfUserIsNotLoggedOut : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var authService =  context.HttpContext.RequestServices.GetService<IAuthService>();

            var token = context.HttpContext.GetTokenAsync("access_token");

            

            if (authService.TokenExists(token.Result))
            {
                context.Result = new UnAuthorizedActionResult();
            }
        }
    }
}

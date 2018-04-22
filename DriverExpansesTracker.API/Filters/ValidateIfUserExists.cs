using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Filters
{
    public class ValidateIfUserExists : ActionFilterAttribute
    { 
        //private readonly IUserService _userService;


        //public ValidateIfUserExists(IUserService userService)
        //{
        //    _userService = userService;
        //}
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    base.On
        //}

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var userId = context.ActionArguments["userId"] as string;
        //    if (userId != null)
        //    {
        //        if (!_userService.UserExists(userId))
        //        {
        //            context.Result = new NotFoundResult();
        //        }
        //    }

        //}
    
    }
}

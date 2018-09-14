using DriverExpansesTracker.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Filters
{
    public class ValidateAuthorizedUserFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var userIdParameter = context.ActionArguments.FirstOrDefault(x => x.Key == "userId").Value as string;

            var authorizedUserId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userIdParameter != null || !string.IsNullOrEmpty(authorizedUserId))
            {
                if (authorizedUserId != userIdParameter)
                {
                    context.Result = new ForbidenActionResult();
                }

            }
            else
            {
                throw new Exception("Wrong usage of ValidateAuthorizedUserFilter. userIdParameter or authorizedUserId is null");
            }


        }
    }
}

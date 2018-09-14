using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Helpers
{
    public class ForbidenActionResult : ActionResult
    {
        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 403;
        }
    }
}

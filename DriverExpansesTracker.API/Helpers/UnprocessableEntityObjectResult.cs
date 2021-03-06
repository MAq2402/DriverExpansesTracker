﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverExpansesTracker.API.Helpers
{
    public class UnprocessableEntityObjectresult: ObjectResult
    {
        public UnprocessableEntityObjectresult(ModelStateDictionary modelState):base(new SerializableError(modelState))
        {
            if (modelState == null)
            {
                throw new ArgumentNullException();
            }
            StatusCode = 422;
        }
    }
}

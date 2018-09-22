using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    public class BaseController : Controller
    {
        protected IUrlHelper _urlHelper;

        public BaseController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        protected string CreateResourceUri(string routeName,ResourceParameters resourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link(routeName, new
                    {
                        pageNumber = resourceParameters.PageNumber - 1,
                        pageSize = resourceParameters.PageSize,
                        search = resourceParameters.Search
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(routeName, new
                    {
                        pageNumber = resourceParameters.PageNumber + 1,
                        pageSize = resourceParameters.PageSize,
                        search = resourceParameters.Search
                    });
                default:
                    return _urlHelper.Link(routeName, new
                    {
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize,
                        search = resourceParameters.Search
                    });
            }
        }
    }
}

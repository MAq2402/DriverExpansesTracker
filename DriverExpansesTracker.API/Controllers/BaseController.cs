using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    public class BaseController : Controller
    {
        protected IUrlHelper _urlHelper;
        protected ILinksService _linksService;

        public BaseController(IUrlHelper urlHelper,ILinksService linksService)
        {
            _urlHelper = urlHelper;
            _linksService = linksService;
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
                        search = resourceParameters.Search,
                        start = resourceParameters.Start,
                        destination = resourceParameters.Destination
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(routeName, new
                    {
                        pageNumber = resourceParameters.PageNumber + 1,
                        pageSize = resourceParameters.PageSize,
                        search = resourceParameters.Search,
                        start = resourceParameters.Start,
                        destination = resourceParameters.Destination
                    });
                default:
                    return _urlHelper.Link(routeName, new
                    {
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize,
                        search = resourceParameters.Search,
                        start = resourceParameters.Start,
                        destination = resourceParameters.Destination
                    });
            }
        }

        protected async Task AddLinksToCollectionAsync<T>(IEnumerable<T> linkContainers) where T: ILinkContainer
        {
            foreach(var linkContainer in linkContainers)
            {
                await _linksService.AddLinksAsync(linkContainer);
            }
        }

        protected void AddPaginationHeader<T>(PagedList<T> pagedList,string routeName,ResourceParameters resourceParameters)
        {
            pagedList.Header.PreviousPageLink = pagedList.HasPrevious ? CreateResourceUri(routeName, resourceParameters, ResourceUriType.PreviousPage) : null;
            pagedList.Header.NextPageLink = pagedList.HasNext ? CreateResourceUri(routeName, resourceParameters, ResourceUriType.NextPage) : null;

            Response.Headers.Add(Constants.Headers.XPagination, pagedList.Header.ToJson());
        }
    }
}

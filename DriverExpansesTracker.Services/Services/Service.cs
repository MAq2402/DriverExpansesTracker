using System;
using System.Collections.Generic;
using System.Text;
using DriverExpansesTracker.Services.Helpers;

namespace DriverExpansesTracker.Services.Services
{
    public class Service<T> : IService<T>
    {
        public PagingHeader<T> GetPagingHeader(string routeName,PagedList<T> pagedList, ResourceParameters resourceParameters, CreateResourceUriDel del)
        {
            var pagingHeader = new PagingHeader<T>(pagedList, resourceParameters);

            pagingHeader.NextPageLink = pagedList.HasNext ? del(routeName, resourceParameters, ResourceUriType.NextPage) : null;
            pagingHeader.PreviousPageLink = pagedList.HasPrevious ? del(routeName, resourceParameters, ResourceUriType.PreviousPage) : null;

            return pagingHeader;
        }
    }
}

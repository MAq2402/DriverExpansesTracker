
using DriverExpansesTracker.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Services
{
    public interface IService<T>
    {
        PagingHeader<T> GetPagingHeader(string routeName,PagedList<T> pagedList, ResourceParameters resourceParameters, CreateResourceUriDel del);
    }
}

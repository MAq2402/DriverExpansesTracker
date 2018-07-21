using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Helpers
{
    public class PagingHeader<T>
    {
        public PagingHeader(PagedList<T> pagedList,ResourceParameters resourceParameters)
        {
            TotalCount = pagedList.TotalCount;
            PageSize = pagedList.PageSize;
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
        }
        public int TotalCount { get;private set; }
        public int PageSize { get;private set; }
        public int CurrentPage { get;private set; }
        public int TotalPages { get;private set; }
        public string PreviousPageLink { get; set; }
        public string NextPageLink { get; set; }
        public string ToJson()
        {

                return JsonConvert.SerializeObject(this);
           
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Helpers
{
    public class PagingHeader
    {
        public PagingHeader(int totalCount, int pageSize, int currentPage, int totalPages)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
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

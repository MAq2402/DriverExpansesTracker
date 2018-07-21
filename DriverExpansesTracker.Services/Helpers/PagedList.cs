using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriverExpansesTracker.Services.Helpers
{
    public class PagedList<T>:List<T>
    {
        
        public int CurrentPage { get;private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPages;
            }
        }

        public PagedList(IQueryable<T> query,int pageNumber,int pageSize )
        {
            CurrentPage = pageNumber;
            TotalCount = query.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            PageSize = pageSize;
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            AddRange(items);          
        }
    }
}

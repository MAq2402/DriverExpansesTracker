using System;
using System.Collections.Generic;
using System.Text;

namespace DriverExpansesTracker.Services.Helpers
{
    public class ResourceParameters
    {
        protected const int maxPageSize = 10;

        protected int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }

        public string Search { get; set; }

        public string Start { get; set; }
        public string Destination { get; set; }
    }
}

using System.Collections.Generic;

namespace SmartCqrs.Infrastructure.DapperEx
{
    public class PagedData<T> 
    {
        public int TotalCount { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}

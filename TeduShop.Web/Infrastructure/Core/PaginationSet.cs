using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Infrastructure.Core
{
    public class PaginationSet<T> where T : class
    {
        public int Page { get; set; }

        public int Count => (Items != null) ? Items.Count() : 0;
        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
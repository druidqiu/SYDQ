using System.Collections.Generic;
using System.Linq;

namespace SYDQ.Infrastructure.Pager
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> superset, int pageIndex, int pageSize = 10)
        {
            return new PagedList<T>(superset, pageIndex, pageSize);
        }

        public static IPagedList<T> ToPagedList<T>(this IOrderedQueryable<T> superset, int pageIndex, int pageSize = 10)
        {
            return new PagedList<T>(superset, pageIndex, pageSize);
        }
    }
}

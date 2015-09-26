using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    internal static class PageExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IOrderedQueryable<T> allItems, int pageIndex, int pageSize)
        {
            return new PagedList<T>(allItems, pageIndex, pageSize);
        }
    }
}

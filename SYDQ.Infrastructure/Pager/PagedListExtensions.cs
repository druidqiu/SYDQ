using System;
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

        public static IPagedList<T> ToPagedList<T>(this IOrderedQueryable<T> superset, int pageIndex, int pageSize)
        {
            return new PagedList<T>(superset, pageIndex, pageSize);
        }

        private static IPagedList<T> ToPagedList<T>(this IQueryable<T> superset,
            int pageIndex, int pageSize, string sortField, SortOrder sortOrder)
        {
            return new PagedList<T>(superset, pageIndex,
                pageSize, sortField, sortOrder);
        }

        public static IPagedList<T1> ToPagedList<T, T1>(this IOrderedQueryable<T> superset,
            int pageIndex, int pageSize,
            Func<IEnumerable<T>, IEnumerable<T1>> converter)
        {
            var pagedList = superset.ToPagedList(pageIndex, pageSize);
            var newList = converter(pagedList);
            return new StaticPagedList<T1>(newList, pagedList.GetMetaData());
        }

        public static IPagedList<T1> ToPagedList<T, T1>(this IQueryable<T> superset,
            int pageIndex, int pageSize,
            string sortField, SortOrder sortOrder,
            Func<IEnumerable<T>, IEnumerable<T1>> converter)
        {
            IPagedList<T> pagedList;

            string validSortField =
                typeof(T).GetProperties().Any(p => p.Name.ToLower() == sortField.ToLower()) ? sortField : "Id";
            pagedList = superset.ToPagedList(pageIndex, pageSize, validSortField, sortOrder);

            var newList = converter(pagedList);
            return new StaticPagedList<T1>(newList, pagedList.GetMetaData());
        }
    }
}

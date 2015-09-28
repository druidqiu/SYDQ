﻿using SYDQ.Infrastructure.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Services
{
    public static class PagedListExtension
    {
        public static IPagedList<T1> ToPagedList<T, T1>(this IOrderedQueryable<T> superset,
            int pageIndex, int pageSize,
            Func<IEnumerable<T>, IEnumerable<T1>> converter)
        {
            var pagedList = superset.ToPagedList(pageIndex, pageSize);
            var newList = converter(pagedList);
            return new StaticPagedList<T1>(converter(pagedList), pagedList.GetMetaData());
        }
    }
}

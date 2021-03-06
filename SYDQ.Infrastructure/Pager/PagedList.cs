﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace SYDQ.Infrastructure.Pager
{
    public class PagedList<T> : BasePagedList<T>
    {
        public PagedList(IQueryable<T> superset, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
                //throw new ArgumentOutOfRangeException(String.Format("pageIndex = {0}. PageIndex cannot be below 1.", pageIndex));

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(String.Format("pageSize = {0}. PageSize cannot be less than 1.", pageSize));

            TotalItemCount = superset == null ? 0 : superset.Count();
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                        : 0;
            if (pageIndex > TotalPageCount) PageIndex = TotalPageCount;
            HasPreviousPage = PageIndex > 1;
            HasNextPage = PageIndex < TotalPageCount;
            IsFirstPage = PageIndex == 1;
            IsLastPage = PageIndex >= TotalPageCount;
            FirstItemOnPage = (PageIndex - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                            ? TotalItemCount
                            : numberOfLastItemOnPage;

            if (superset != null && TotalItemCount > 0)
                Subset.AddRange(superset.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public PagedList(IEnumerable<T> superset, int pageIndex, int pageSize)
            : this(superset.AsQueryable(), pageIndex, pageSize)
        {
        }

        public PagedList(IQueryable<T> superset, int pageIndex, int pageSize, string sortField, SortOrder sortOrder)
        {
            if (pageIndex < 1)
                pageIndex = 1;
                //throw new ArgumentOutOfRangeException(String.Format("pageIndex = {0}. PageIndex cannot be below 1.", pageIndex));
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(String.Format("pageSize = {0}. PageSize cannot be less than 1.", pageSize));
            if (string.IsNullOrEmpty(sortField))
                throw new ArgumentNullException(String.Format("SortField cannot be empty."));


            TotalItemCount = superset == null ? 0 : superset.Count();
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                        : 0;
            if (pageIndex > TotalPageCount) PageIndex = TotalPageCount;
            HasPreviousPage = PageIndex > 1;
            HasNextPage = PageIndex < TotalPageCount;
            IsFirstPage = PageIndex == 1;
            IsLastPage = PageIndex >= TotalPageCount;
            FirstItemOnPage = (PageIndex - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                            ? TotalItemCount
                            : numberOfLastItemOnPage;
            SortField = sortField;
            SortOrder = sortOrder;

            if (superset != null && TotalItemCount > 0)
                Subset.AddRange(superset.OrderBy(GetSortString()).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
    }
}

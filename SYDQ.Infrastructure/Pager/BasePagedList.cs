using System;
using System.Collections;
using System.Collections.Generic;

namespace SYDQ.Infrastructure.Pager
{
    public abstract class BasePagedList<T> : PagedListMetaData, IPagedList<T>
    {
        protected readonly List<T> Subset = new List<T>();

        protected internal BasePagedList()
        {
        }

        protected internal BasePagedList(int pageIndex, int pageSize, int totalItemCount, string sortField, SortOrder sortOrder)
        {
            if (pageIndex < 1)
                pageIndex = 1;
                //throw new ArgumentOutOfRangeException(String.Format("pageIndex = {0}. PageIndex cannot be below 1.", pageIndex));
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(String.Format("pageSize = {0}. PageSize cannot be less than 1.", pageSize));
            if (string.IsNullOrEmpty(sortField))
                throw new ArgumentNullException(String.Format("SortField cannot be empty."));


            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = totalItemCount;
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
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Subset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get { return Subset[index]; }
        }

        public IQueryOptions GetMetaData()
        {
            return new PagedListMetaData(this);
        }

    }
}

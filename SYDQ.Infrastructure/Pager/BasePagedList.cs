using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    public abstract class BasePagedList<T> : PagedListMetaData, IPagedList<T>
    {
        protected readonly List<T> Subset = new List<T>();

        protected internal BasePagedList()
        {
        }

        protected internal BasePagedList(int pageIndex, int pageSize, int totalItemCount)
        {
            if (pageIndex < 1)
                throw new ArgumentOutOfRangeException(String.Format("pageIndex = {0}. PageIndex cannot be below 1.", pageIndex));
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(String.Format("pageSize = {0}. PageSize cannot be less than 1.", pageSize));

            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            PageCount = TotalItemCount > 0
                            ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                            : 0;
            HasPreviousPage = PageIndex > 1;
            HasNextPage = PageIndex < PageCount;
            IsFirstPage = PageIndex == 1;
            IsLastPage = PageIndex >= PageCount;
            FirstItemOnPage = (PageIndex - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                                ? TotalItemCount
                                : numberOfLastItemOnPage;
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

        public IPagedList GetMetaData()
        {
            return new PagedListMetaData(this);
        }

    }
}

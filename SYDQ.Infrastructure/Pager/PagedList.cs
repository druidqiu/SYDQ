using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    internal class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IOrderedQueryable<T> allItems, int pageIndex, int pageSize)
        {
            TotalItemCount = allItems.Count();
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;

            if (pageSize < 1)
            {
                PageSize = 10;
            }
            if (pageIndex > TotalPageCount)
            {
                pageIndex = TotalPageCount;
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int startIndex = (CurrentPageIndex - 1) * PageSize;
            AddRange(allItems.Skip(startIndex).Take(pageSize));
        }

        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); } }
        public int StartItemIndex { get { return (CurrentPageIndex - 1) * PageSize + 1; } }
        public int EndItemIndex { get { return TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount; } }
    }
}

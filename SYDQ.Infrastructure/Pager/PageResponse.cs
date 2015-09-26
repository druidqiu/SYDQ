using SYDQ.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYDQ.Infrastructure.Pager
{
    public class PageResponse<T> : Response
    {
        public PagerOptions PagerOptions { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PageResponse()
        {
            PagerOptions = new PagerOptions();
        }
        public PageResponse<T> Paging(PageRequest request, IOrderedQueryable<T> items)
        {
            var pagedList = items.ToPagedList(request.PageIndex, request.PageSize);
            request.PageIndex = pagedList.CurrentPageIndex;
            request.PageSize = pagedList.PageSize;

            PagerOptions.PageIndex = pagedList.CurrentPageIndex;
            PagerOptions.PageSize = pagedList.PageSize;
            PagerOptions.PageCount = pagedList.TotalPageCount;
            PagerOptions.ItemCount = pagedList.TotalItemCount;
            PagerOptions.RouteQueries = request.GetRouteQueries();
            
            this.Items = pagedList.ToList();

            return this;
        }

        public void ConvertPaging<M>(PageRequest request,
            IOrderedQueryable<M> items, Func<M, T> converter)
        {
            var pagedList = items.ToPagedList(request.PageIndex, request.PageSize);
            request.PageIndex = pagedList.CurrentPageIndex;
            request.PageSize = pagedList.PageSize;

            PagerOptions.PageIndex = pagedList.CurrentPageIndex;
            PagerOptions.PageSize = pagedList.PageSize;
            PagerOptions.PageCount = pagedList.TotalPageCount;
            PagerOptions.ItemCount = pagedList.TotalItemCount;
            PagerOptions.RouteQueries = request.GetRouteQueries();

            this.Items = pagedList.ConvertAll(new Converter<M, T>(converter));
        }
    }
}

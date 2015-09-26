﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    public class PagedListMetaData : IPagedList
    {
        protected PagedListMetaData()
        { }

        public PagedListMetaData(IPagedList pagedList)
        {
            PageCount = pagedList.PageCount;
            TotalItemCount = pagedList.TotalItemCount;
            PageIndex = pagedList.PageIndex;
            PageSize = pagedList.PageSize;
            HasPreviousPage = pagedList.HasPreviousPage;
            HasNextPage = pagedList.HasNextPage;
            IsFirstPage = pagedList.IsFirstPage;
            IsLastPage = pagedList.IsLastPage;
            FirstItemOnPage = pagedList.FirstItemOnPage;
            LastItemOnPage = pagedList.LastItemOnPage;
        }
        public int PageCount { get; protected set; }
        public int TotalItemCount { get; protected set; }
        public int PageIndex { get; protected set; }
        public int PageSize { get; protected set; }
        public bool HasPreviousPage { get; protected set; }
        public bool HasNextPage { get; protected set; }
        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }
        public int FirstItemOnPage { get; protected set; }
        public int LastItemOnPage { get; protected set; }
    }
}
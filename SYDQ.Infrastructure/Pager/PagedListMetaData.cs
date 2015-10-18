namespace SYDQ.Infrastructure.Pager
{
    public class PagedListMetaData : IQueryOptions
    {
        protected PagedListMetaData()
        { }

        public PagedListMetaData(IQueryOptions pagedList)
        {
            TotalPageCount = pagedList.TotalPageCount;
            TotalItemCount = pagedList.TotalItemCount;
            PageIndex = pagedList.PageIndex;
            PageSize = pagedList.PageSize;
            HasPreviousPage = pagedList.HasPreviousPage;
            HasNextPage = pagedList.HasNextPage;
            IsFirstPage = pagedList.IsFirstPage;
            IsLastPage = pagedList.IsLastPage;
            FirstItemOnPage = pagedList.FirstItemOnPage;
            LastItemOnPage = pagedList.LastItemOnPage;
            SortField = pagedList.SortField;
            SortOrder = pagedList.SortOrder;
        }
        public int TotalPageCount { get; protected set; }
        public int TotalItemCount { get; protected set; }
        public int PageIndex { get; protected set; }
        public int PageSize { get; protected set; }
        public bool HasPreviousPage { get; protected set; }
        public bool HasNextPage { get; protected set; }
        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }
        public int FirstItemOnPage { get; protected set; }
        public int LastItemOnPage { get; protected set; }
        public string SortField { get; protected set; }
        public SortOrder SortOrder { get; protected set; }

        public string GetSortString()
        {
            return string.Format("{0} {1}", SortField, SortOrder);
        }
    }
}

using System.Collections.Generic;

namespace SYDQ.Infrastructure.Pager
{
    public interface IQueryOptions
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPageCount { get; }
        int TotalItemCount { get; }

        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
        int FirstItemOnPage { get; }
        int LastItemOnPage { get; }

        string SortField { get; }
        SortOrder SortOrder { get; }

    }

    public interface IPagedList<out T> : IQueryOptions, IEnumerable<T>
    {
        T this[int index] { get; }
        IQueryOptions GetMetaData();
    }

    public enum SortOrder
    {
        Asc = 0,
        Desc = 1
    }
}

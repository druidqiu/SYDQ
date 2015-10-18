using System.Collections.Generic;

namespace SYDQ.Infrastructure.Pager
{
    public class StaticPagedList<T> : BasePagedList<T>
    {
        public StaticPagedList(IEnumerable<T> subset, IQueryOptions metaData)
            : this(subset, metaData.PageIndex, metaData.PageSize, metaData.TotalItemCount,
            metaData.SortField, metaData.SortOrder)
        {
        }

        public StaticPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount,
            string sortField, SortOrder sortOrder)
            : base(pageNumber, pageSize, totalItemCount,sortField,sortOrder)
        {
            Subset.AddRange(subset);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    internal interface IPagedList : IEnumerable
    {
        int CurrentPageIndex { get; set; }
        int PageSize { get; set; }
        int TotalItemCount { get; set; }
    }

    internal interface IPagedList<T> : IEnumerable<T>, IPagedList
    { }
}

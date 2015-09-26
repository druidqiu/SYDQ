﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    public class StaticPagedList<T> : BasePagedList<T>
    {
        public StaticPagedList(IEnumerable<T> subset, IPagedList metaData)
            : this(subset, metaData.PageIndex, metaData.PageSize, metaData.TotalItemCount)
        {
        }

        public StaticPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount)
            : base(pageNumber, pageSize, totalItemCount)
        {
            Subset.AddRange(subset);
        }
    }
}
using SYDQ.Infrastructure.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYDQ.Web.Areas.Client.Models
{
    public class PagedViewModel
    {
        public IPagedList PagerMetaData { get; set; }
    }
}
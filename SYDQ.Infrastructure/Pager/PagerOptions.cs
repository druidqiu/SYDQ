using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    public class PagerOptions
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int ItemCount { get; set; }
        public int PageCount { get; set; }
        public string TargetId { get; set; }
        public List<Tuple<string,object>> RouteQueries { get; set; }

    }
}

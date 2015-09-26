using SYDQ.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    public class PageRequest : Request
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public List<Tuple<string,object>> GetRouteQueries()
        {
            var routeQueries = new List<Tuple<string,object>>();
            var properties = this.GetType().GetProperties();
            foreach (var p in properties)
            {
                foreach (var attr in p.GetCustomAttributes(true))
                {
                    Attribute queryField = attr as QueryFieldAttribute;
                    if (queryField != null)
                    {
                        var value = p.GetValue(this, null);
                        if (value != null)
                        {
                            routeQueries.Add(new Tuple<string, object>(p.Name, value));
                        }
                    }
                }
            }
            return routeQueries;
        }
    }
}

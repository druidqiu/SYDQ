using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace SYDQ.Controllers.Controllers.Admin
{
    public class AdminBaseController : Controller
    {
        [Obsolete]
        protected void RecordSearchItems(object queries)
        {
            RouteValueDictionary route = new RouteValueDictionary();

            foreach (PropertyInfo info in queries.GetType().GetProperties())
            {
                route.Add(info.Name, info.GetValue(queries, null));
            }

            ViewBag.RouteData = route;
        }
    }
}

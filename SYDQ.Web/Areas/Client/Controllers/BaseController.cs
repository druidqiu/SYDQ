using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SYDQ.Web.Areas.Client.Controllers
{
    public class BaseController : Controller
    {
        protected void RecordSearchItems(object queries)
        {
            RouteValueDictionary route = new RouteValueDictionary();

            foreach (PropertyInfo info in queries.GetType().GetProperties())
            {
                route.Add(info.Name, info.GetValue(queries,null));    
            }
            
            ViewBag.RouteData = route;
        }
    }
}
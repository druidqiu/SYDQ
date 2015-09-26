using SYDQ.Infrastructure.Web.Mvc.Filters;
using System.Web;
using System.Web.Mvc;

namespace SYDQ.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}

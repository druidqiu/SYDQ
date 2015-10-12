using System.Web.Mvc;
using SYDQ.Infrastructure.Web.Mvc.Filters;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SYDQ.Infrastructure.Web.Mvc.Filters
{
    public class LoggerFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string actionName = filterContext.RouteData.Values["action"].ToString().ToLower();
            if (controllerName == "account" && actionName == "login")
                return;
            string absoluteUrl = filterContext.HttpContext.Request.Url.AbsolutePath.ToLower();
            string userId = filterContext.HttpContext.User.Identity.Name.ToLower();
            //string logStr = "{0}于{1}访问了页面{2}，controller = {3},action = {4}";
            // ControllerLog.Logger.Info(string.Format(logStr, userId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), absoluteUrl, controllerName, actionName));
        }
    }
}

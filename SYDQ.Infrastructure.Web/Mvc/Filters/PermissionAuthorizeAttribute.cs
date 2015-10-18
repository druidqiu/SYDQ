using System;
using System.Web;
using System.Web.Mvc;
using SYDQ.Infrastructure.Web.Mvc.Session;

namespace SYDQ.Infrastructure.Web.Mvc.Filters
{
    public enum PermissionKey
    {
        Home,
        User
    }

    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        private string PermissionKey { get; set; }

        public PermissionAuthorizeAttribute(PermissionKey permissionKey)
        {
            PermissionKey = permissionKey.ToString();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionHelper.RetrieveUserInfo() == null)
            {
                httpContext.Response.StatusCode = 403;
                return true; //返回false会执行HandleUnauthorizedRequest方法，返回true会把Session失效问题交给RequiresAuthentication处理。
            }
            if (SessionHelper.RetrieveUserInfo().PermissionKeys.Contains(PermissionKey))
            {
                return true;
            }
            else
            {
                httpContext.Response.StatusCode = 401; //无权限代码
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentException("filterContext");
            }
            else
            {
                filterContext.Result = filterContext.HttpContext.Request.IsAjaxRequest() ? new RedirectResult("/Error/AjaxNoAccess") : new RedirectResult("/Error/NoAccess");
            }
        }
    }
}

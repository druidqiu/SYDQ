using SYDQ.Infrastructure.Web.Mvc.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SYDQ.Infrastructure.Web.Mvc.Filters
{
    public enum PermissionKey
    {
        Home,
        User
    }

    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        private string _permissionKey { get; set; }

        public PermissionAuthorizeAttribute(PermissionKey permissionKey)
        {
            _permissionKey = permissionKey.ToString();
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (SessionHelper.RetrieveUserInfo() == null)
            {
                httpContext.Response.StatusCode = 403;
                return true; //返回false会执行HandleUnauthorizedRequest方法，返回true会把Session失效问题交给RequiresAuthentication处理。
            }
            if (SessionHelper.RetrieveUserInfo().PermissionKeys.Contains(_permissionKey))
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
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("/Error/AjaxNoAccess");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Error/NoAccess");
                }
                return;
            }
        }
    }
}

using System;
using System.Web.Mvc;
using System.Web.Security;

namespace SYDQ.Infrastructure.Web.Mvc
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            //CultureSetting.SetCurrentThreadCulture();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //set user info to session
            }
            else
            {
                if (filterContext.HttpContext.Request.Url != null)
                {
                    string redirectOnSuccess = filterContext.HttpContext.Request.Url.PathAndQuery;
                    string redirectUrl = String.Format("?returnUrl={0}", redirectOnSuccess);
                    string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectResult(loginUrl);
                }
                return;
                //filterContext.HttpContext.Response.Redirect(loginUrl, true);//Filter中使用Response.Redirect会继续运行Action.
            }
        }
    }
}

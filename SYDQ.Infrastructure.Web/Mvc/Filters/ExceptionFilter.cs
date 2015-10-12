using System.Web.Mvc;

namespace SYDQ.Infrastructure.Web.Mvc.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            //Logger.Error(exception.ToString());

            filterContext.ExceptionHandled = true;

            UrlHelper url = new UrlHelper(filterContext.RequestContext);
            filterContext.Result = new RedirectResult(url.Action("Index", "Error", new { error = exception.Message }));
        }
    }
}

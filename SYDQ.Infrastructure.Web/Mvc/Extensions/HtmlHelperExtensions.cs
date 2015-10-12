using Newtonsoft.Json;
using SYDQ.Infrastructure.Pager;
using SYDQ.Infrastructure.Web.Mvc.Extensions;
using System.Text;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ShowException(this HtmlHelper htmlHelper)
        {
            var htmlString = string.Empty;
            var exception = htmlHelper.ViewContext.Controller.ViewData["Exception"] as Exception;
            if (exception != null)
            {
                StringBuilder content = new StringBuilder();
                content.Append("<br/>");
                content.Append("<div class=\"accordion\" id=\"accordionException\">");
                content.Append("<div class=\"panel panel-danger\">");
                content.Append("<div class=\"panel-heading\"  style=\"cursor:pointer\">");
                content.Append("<div data-toggle=\"collapse\" data-parent=\"#accordionException\" href=\"#collapseExceptionMessage\">");
                content.Append("Exception Message: "+exception.Message);
                content.Append("</div></div>");
                content.Append("<div id=\"collapseExceptionMessage\" class=\"panel-collapse collapse\">");
                content.Append("<div class=\"panel-body\">");
                content.Append(exception.ToString().Replace("\r\n", "<br/>"));
                content.Append("</div></div></div></div>");
                content.Append("<br/>");

                htmlString = content.ToString();
            }
            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString ShowExceptionPartial(this HtmlHelper htmlHelper)
        {
            var exception = htmlHelper.ViewContext.Controller.ViewData["Exception"] as Exception;
            if (exception != null)
            {
                return htmlHelper.Partial("_ErrorHandlerPartial", exception);
            }
            return new MvcHtmlString("");
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList pagerMetaData)
        {
            var htmlString = string.Empty;
            if (pagerMetaData.TotalPageCount > 1)
            {
                var controllerName =htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
                var actionName = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
                int pageIndex = pagerMetaData.PageIndex;
                int pageCount = pagerMetaData.TotalPageCount;
                const int displayNum = 5;
                const int intervalNum = displayNum / 2;
                var startPageNum = pageCount <= displayNum ? 1 : ((pageIndex + intervalNum) >= pageCount ? (pageCount - displayNum + 1) : ((pageIndex - intervalNum) > 0 ? (pageIndex - intervalNum) : 1));
                var endPageNum = pageCount <= displayNum ? pageCount : ((startPageNum + displayNum - 1) > pageCount ? pageCount : (startPageNum + displayNum - 1));

                RouteValueDictionary routeData = new RouteValueDictionary();
                routeData.AddQueryStringParameters();

                StringBuilder content = new StringBuilder();
                routeData.Remove("page");
                routeData.Add("page", 1);
                content.Append("<ul class=\"pagination pull-right\">");

                //turn to first page
                if (pageIndex == 1)
                {
                    content.Append("<li class=\"disabled\"><a><<</a></li>");
                }
                else
                {
                    content.Append("<li>")
                        .Append(htmlHelper.ActionLink("<<", actionName, controllerName, routeData, null).ToHtmlString())
                        .Append("</li>");
                }

                for (int i = startPageNum; i <= endPageNum; i++)
                {
                    routeData["page"] = i;
                    bool isActive = pageIndex == i;
                    content.Append(isActive?"<li class=\"active\">":"<li>")
                        .Append(htmlHelper.ActionLink(i.ToString(), actionName, controllerName, routeData, null).ToHtmlString())
                        .Append("</li>");
                }

                //turn to last page
                routeData["page"] = pageCount;
                if (pageIndex == pageCount)
                {
                    content.Append("<li class=\"disabled\"><a>>></a></li>");
                }
                else
                {
                    content.Append("<li>")
                        .Append(htmlHelper.ActionLink(">>", actionName, controllerName, routeData, null).ToHtmlString())
                        .Append("</li>");
                }

                content.Append("<li class=\"disabled\"><a>").Append(pageIndex.ToString()).Append("/").Append(pageCount.ToString()).Append("</a></li>");

                htmlString = content.ToString();
            }

            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString PagerPartial(this HtmlHelper htmlHelper, IPagedList pagerMetaData)
        {
            return htmlHelper.Partial("_PagerPartial", pagerMetaData);
        }


        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }

        public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper,
            string fieldName, string actionName, string sortField, QueryOptions queryOptions)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var isCurrentSortField = queryOptions.SortField == sortField;

            return new MvcHtmlString(string.Format("<a href=\"{0}\">{1} {2}</a>",
              urlHelper.Action(actionName,
              new
              {
                  SortField = sortField,
                  SortOrder = (isCurrentSortField
                      && queryOptions.SortOrder == SortOrder.Asc)
                    ? SortOrder.Desc : SortOrder.Asc
              }),
              fieldName,
              BuildSortIcon(isCurrentSortField, queryOptions)));
        }

        private static string BuildSortIcon(bool isCurrentSortField
      , QueryOptions queryOptions)
        {
            string sortIcon = "sort";

            if (isCurrentSortField)
            {
                sortIcon += "-by-alphabet";
                if (queryOptions.SortOrder == SortOrder.Desc)
                    sortIcon += "-alt";
            }

            return string.Format("<span class=\"{0} {1}{2}\"></span>", "glyphicon", "glyphicon-", sortIcon);
        }
    }
}

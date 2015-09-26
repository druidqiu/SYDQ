using Newtonsoft.Json;
using SYDQ.Infrastructure.Pager;
using SYDQ.Infrastructure.Web.Mvc.Extensions;
using System.Text;
using System.Web.Mvc.Html;

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
                return Html.PartialExtensions.Partial(htmlHelper, "_ErrorHandlerPartial", exception);
            }
            return new MvcHtmlString("");
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList pagerMetaData)
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
                      && queryOptions.SortOrder == SortOrder.ASC)
                    ? SortOrder.DESC : SortOrder.ASC
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
                if (queryOptions.SortOrder == SortOrder.DESC)
                    sortIcon += "-alt";
            }

            return string.Format("<span class=\"{0} {1}{2}\"></span>", "glyphicon", "glyphicon-", sortIcon);
        }
    }
}

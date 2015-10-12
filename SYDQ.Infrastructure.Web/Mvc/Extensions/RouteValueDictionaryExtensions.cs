using System.Web;
using System.Web.Routing;

namespace SYDQ.Infrastructure.Web.Mvc.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        public static RouteValueDictionary AddQueryStringParameters(this RouteValueDictionary dict)
        {
            var querystring = HttpContext.Current.Request.QueryString;

            foreach (var key in querystring.AllKeys)
                if (key != null && !dict.ContainsKey(key))
                {
                    var values = querystring.GetValues(key);
                    if (values != null)
                        dict.Add(key, values[0]);
                }

            return dict;
        }

        public static RouteValueDictionary ExceptFor(this RouteValueDictionary dict, params string[] keysToRemove)
        {
            foreach (var key in keysToRemove)
                if (dict.ContainsKey(key))
                    dict.Remove(key);

            return dict;
        }
    }
}
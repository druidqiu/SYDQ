﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SYDQ.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", acion = "Login" },
                namespaces: new string[] { "SYDQ.Controllers.Controllers" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SYDQ.Controllers.Controllers.Admin" }
            ).DataTokens.Add("area", "Admin");

            routes.MapRoute(
                name: "Client",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SYDQ.Controllers.Controllers.Client" }
            ).DataTokens.Add("area", "Client");

        }
    }
}

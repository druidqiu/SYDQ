using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using SYDQ.Repository.EF;
using SYDQ.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SYDQ.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterAll();
            //DbInterception.Add(new EFIntercepterLogging());
            MiniProfilerEF6.Initialize();

        }

        protected void Application_Error()
        {
            ApplicationErrorHandler.Handler(Server, Response, Context);
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Application_End()
        {
            //RestartWebApp();
        }

        private void RestartWebApp()
        {
            try
            {
                Thread.Sleep(5000);
                string baseUrl = SYDQ.Infrastructure.Configuration.ApplicationSettingsFactory.GetApplicationSettings().WebRootUrl;
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl);
                myHttpWebRequest.UseDefaultCredentials = true;

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream receiveStream = myHttpWebResponse.GetResponseStream();
                myHttpWebResponse.Close();
            }
            catch { }
        }
    }
}

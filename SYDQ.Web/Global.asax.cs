using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using SYDQ.Infrastructure.Configuration;
using SYDQ.Web.App_Start;

namespace SYDQ.Web
{
    public class MvcApplication : HttpApplication
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
                string baseUrl = ApplicationSettingsFactory.GetApplicationSettings().WebRootUrl;
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

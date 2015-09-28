using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SYDQ.Web.App_Start
{
    public class ApplicationErrorHandler
    {//TODO: need to update
        public static void Handler(HttpServerUtility Server, HttpResponse Response, HttpContext Context)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Log");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fileFullName = System.IO.Path.Combine(path, fileName);
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(fileFullName, true);
                writer.WriteLine("Exception at " + DateTime.Now.ToString() + ":");
                writer.WriteLine(Server.GetLastError().Source);
                writer.WriteLine(Server.GetLastError().Message);
                writer.WriteLine(Server.GetLastError().TargetSite);
                writer.Close();

                Exception exception = Server.GetLastError();
                Response.Clear();
                HttpException httpException = exception as HttpException;
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                if (httpException == null)
                {
                    routeData.Values.Add("action", "Index");
                }
                else
                {
                    switch (httpException.GetHttpCode())
                    {
                        case 404:
                            //Page not found.
                            routeData.Values.Add("action", "HttpError404");
                            break;
                        case 500:
                            //Server error.
                            routeData.Values.Add("action", "HttpError500");
                            break;
                        default:
                            routeData.Values.Add("action", "General");
                            break;
                    }
                }
                routeData.Values.Add("error", exception.Message);
                Server.ClearError();
                //IController errorController = new ErrorsController();
                //errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
            catch (Exception)
            {

            }
        }
    }
}
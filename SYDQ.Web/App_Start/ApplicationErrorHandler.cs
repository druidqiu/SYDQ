using System;
using System.IO;
using System.Web;
using System.Web.Routing;

namespace SYDQ.Web.App_Start
{
    public class ApplicationErrorHandler
    {//TODO: need to update
        public static void Handler(HttpServerUtility server, HttpResponse response, HttpContext context)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Log");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileFullName = Path.Combine(path, fileName);
            try
            {
                StreamWriter writer = new StreamWriter(fileFullName, true);
                writer.WriteLine("Exception at " + DateTime.Now + ":");
                writer.WriteLine(server.GetLastError().Source);
                writer.WriteLine(server.GetLastError().Message);
                writer.WriteLine(server.GetLastError().TargetSite);
                writer.Close();

                Exception exception = server.GetLastError();
                response.Clear();
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
                server.ClearError();
                //IController errorController = new ErrorsController();
                //errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
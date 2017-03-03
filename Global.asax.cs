using ListBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ListBlog
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;
            RouteData route = new RouteData();
            route.Values.Add("controller", "Error");
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        route.Values.Add("action", "Http404");
                        break;
                    case 500:
                        route.Values.Add("action", "Http500");
                        break;
                    default:
                        route.Values.Add("action", "General");
                        break;
                }
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                IController errorcontroller = new ErrorController();
                errorcontroller.Execute(new RequestContext(new HttpContextWrapper(Context), route));

            }

        }
    }
}
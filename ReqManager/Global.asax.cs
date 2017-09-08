using ReqManager.App_Start;
using ReqManager.Services.Acess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ReqManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Run();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
                RouteData rd = RouteTable.Routes.GetRouteData(context);

                if (rd != null)
                {
                    string controller = rd.GetRequiredString("controller");
                    string action = rd.GetRequiredString("action");
                }
                else
                {
                    HttpContext.Current.RewritePath("Home/About");
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

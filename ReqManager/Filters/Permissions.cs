using ReqManager.Services.InterfacesServices;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ReqManager.Filters
{
    public class Permissions : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                bool hasPermission = false;

                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    string[] roles = authTicket.UserData.Split(new char[] { '|' });

                    foreach (var item in roles)
                    {
                        string[] controllerAction = item.Split(':');
                        if(controllerAction[0].Equals(controllerName) && controllerAction[1].Equals(actionName))                        
                            hasPermission = true;                        
                    }

                    IIdentity id = new FormsIdentity(authTicket);
                    IPrincipal principal = new GenericPrincipal(id, roles);
                    HttpContext.Current.Request.RequestContext.HttpContext.User = principal;

                    if(!hasPermission)
                    {
                        filterContext.Result = new HttpUnauthorizedResult(
                            "You don't have Permissions to Access " + actionName + " " + controllerName);
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Login" },
                            { "action", "Login" }
                        });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
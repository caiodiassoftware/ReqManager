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

                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    IControllerActionService caService = DependencyResolver.Current.GetService<IControllerActionService>();

                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    int UserID = Convert.ToInt32(authTicket.UserData);

                    IIdentity id = new FormsIdentity(authTicket);
                    IPrincipal principal = new GenericPrincipal(id, null);
                    HttpContext.Current.Request.RequestContext.HttpContext.User = principal;

                    if (!caService.CanAccess(UserID, controllerName, actionName))
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
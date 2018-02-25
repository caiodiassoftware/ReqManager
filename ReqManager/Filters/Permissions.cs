using ReqManager.Entities;
using ReqManager.Entities.Acess;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ReqManager.Filters
{
    public class Permissions : ActionFilterAttribute, IActionFilter
    {
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
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
                    IUserService userService = DependencyResolver.Current.GetService<IUserService>();
                    List<ControllerActionEntity> cas = HttpContext.Current.Session["permissions"] as List<ControllerActionEntity>;

                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    if (!authTicket.Expired && cas != null)
                    {
                        int UserID = Convert.ToInt32(authTicket.UserData);

                        UserEntity user = userService.get(UserID);
                        HttpContext.Current.Session["name"] = user.nickName;

                        authCookie.Expires = DateTime.Now.AddMinutes(30);
                        HttpContext.Current.Response.Cookies.Add(authCookie);

                        IIdentity id = new FormsIdentity(authTicket);
                        IPrincipal principal = new GenericPrincipal(id, null);
                        HttpContext.Current.Request.RequestContext.HttpContext.User = principal;

                        if (!caService.CanAccess(cas, controllerName, actionName))                        
                            throw new Exception("You don't have Permissions to " + actionName + " " + controllerName);

                        if(controllerName.Equals("Login") && actionName.Equals("Login"))
                        {
                            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Requirement" },
                            { "action", "Index" }
                        });
                        }
                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                        authCookie.Expires = DateTime.Now.AddYears(-1);
                        HttpContext.Current.Response.Cookies.Add(authCookie);

                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Login" },
                            { "action", "Login" }
                        });
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
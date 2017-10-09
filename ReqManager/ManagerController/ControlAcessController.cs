using ReqManager.Entities.Acess;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.ManagerController
{
    public class ControlAcessController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                if (Session["user"] != null)
                {
                    UserEntity user = (UserEntity)Session["user"];
                    List<ControllerActionViewModel> controllerActions = (List<ControllerActionViewModel>)Session["controllerActions"];

                    if (controllerActions.Where(x => x.Action.Equals(actionName) &&
                    x.Controller.Equals(controllerName + "Controller")).Count().Equals(0))
                    {
                        actionName = "Error";
                        controllerName = "Shared";
                    }
                }
                else
                {
                    actionName = "Login";
                    controllerName = "Login";
                }

                //filterContext.Result = new RedirectToRouteResult(
                //new RouteValueDictionary
                //{ { "Controller", controllerName },
                //    { "Action", actionName } });
                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
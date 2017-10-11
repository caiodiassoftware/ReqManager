using ReqManager.Entities.Acess;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.ManagerController
{
    public class ControlAcessController<TEntity> : Controller where TEntity: class
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

        #region Protected and Private Methods

        protected int getIdUser()
        {
            return 1;
        }

        protected void setIdUser(ref TEntity entity)
        {
            try
            {
                PropertyInfo prop = entity.GetType().GetProperty("UserID", BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(entity, getIdUser(), null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string getLoginUser()
        {
            return "caiodias";
        }

        protected ActionResult getMessageDbValidation(TEntity entity, DbEntityValidationException ex)
        {
            var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
            ViewBag.MessageReqManager = String.Format("Error Detected in DataBase validation! " + fullErrorMessage);
            return View(entity);
        }

        protected ActionResult getMessageDbUpdateException(TEntity entity, DbUpdateException ex)
        {
            var builder = new StringBuilder("Erro was detected while saving changes. ");

            try
            {
                foreach (var result in ex.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            ViewBag.MessageReqManager = message;
            return View(entity);
        }

        protected ActionResult getMessageGeralException(TEntity entity, Exception ex)
        {
            ViewBag.MessageReqManager = String.Format("Error Detected! " + ex.Message);
            return View(entity);
        }

        protected void getModelStateValidations()
        {
            ViewBag.MessageReqManager = String.Concat("Error Detected in View validation! ", string.Join("; ", ModelState.Values
                                                    .SelectMany(x => x.Errors)
                                                    .Select(x => x.ErrorMessage)));
        }

        #endregion
    }
}
using DataTables.Mvc;
using ReqManager.Entities.Acess;
using ReqManager.Services.Estructure;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ReqManager.ManagerController
{
    public class ControlAcessController<TEntity> : Controller where TEntity : class
    {
        #region Attributes

        protected IService<TEntity> Service { get; set; }

        #endregion

        public ControlAcessController(IService<TEntity> service)
        {
            this.Service = service;
        }

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

        #region Filters

        public ActionResult Get(int? ID)
        {
            try
            {
                return (ID != 0) ? Json(Service.get(Convert.ToInt32(ID)), JsonRequestBehavior.AllowGet) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetFilter([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
                List<TEntity> characteristics = Service.getAll().ToList();
                List<TEntity> result = new List<TEntity>();
                string searchValue = requestModel.Search.Value;

                if (!string.IsNullOrEmpty(searchValue))
                {
                    foreach (TEntity item in characteristics)
                    {
                        foreach (PropertyInfo pi in item.GetType().GetProperties())
                        {
                            string value = pi.GetValue(item).ToString();

                            string json = new JavaScriptSerializer().Serialize(item).ToLower(); ;
                            if (json.Contains(searchValue.ToLower()))
                            {
                                result.Add(item);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    result = characteristics.Take(5).ToList();
                }

                return Json(new
                {
                    //sEcho = requestModel.Search.Value,
                    iTotalRecords = result.Count(),
                    iTotalDisplayRecords = result.Count(),
                    aaData = result
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

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
                    builder.AppendFormat(!string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : ex.Message);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString() + " - " + ex.InnerException.InnerException.Message;
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
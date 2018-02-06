using DataTables.Mvc;
using ReqManager.Filters;
using ReqManager.Notifications.Classes;
using ReqManager.Notifications.Interfaces;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ReqManager.ManagerController
{
    [Permissions]
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class ControlAccessController<TEntity> : Controller where TEntity : class
    {
        #region Attributes

        protected IService<TEntity> Service { get; set; }
        protected INotifierService notifier { get; set; }

        #endregion

        #region Constructor

        public ControlAccessController(IService<TEntity> service)
        {
            this.Service = service;
            notifier = DependencyResolver.Current.GetService<INotifierService>();
        }

        #endregion

        #region Gets

        public virtual ActionResult Get(int? ID)
        {
            try
            {
                return (ID != 0 && ID != null) ? Json(Service.get(Convert.ToInt32(ID)), JsonRequestBehavior.AllowGet) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ActionResult GetAll()
        {
            try
            {
                return Json(Service.getAll(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ActionResult GetFilter([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
                List<TEntity> result = new List<TEntity>();
                string searchValue = requestModel.Search.Value;

                if (!string.IsNullOrEmpty(searchValue))
                {
                    List<TEntity> entities = Service.getAll().ToList();

                    foreach (TEntity item in entities)
                    {
                        foreach (PropertyInfo pi in item.GetType().GetProperties())
                        {
                            string json = new JavaScriptSerializer().Serialize(item).ToLower();
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
                    result = Service.getAll(10).ToList();
                }

                return Json(new
                {
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
            try
            {
                return Convert.ToInt32(getAuthToken().UserData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected string getLoginUser()
        {
            try
            {
                return getAuthToken().Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void setCreationDate(ref TEntity entity)
        {
            try
            {
                PropertyInfo prop = entity.GetType().
                    GetProperty("creationDate",
                    BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(entity, DateTime.Now, null);
                prop = entity.GetType().
                    GetProperty("CreationDate",
                    BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(entity, DateTime.Now, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void setIdUser(ref TEntity entity)
        {
            try
            {
                PropertyInfo prop = entity.GetType().
                    GetProperty("CreationUserID", 
                    BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(entity, getIdUser(), null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private FormsAuthenticationTicket getAuthToken()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket;
        }

        protected ActionResult filterException(Exception ex)
        {
            if (ex is DbEntityValidationException)
            {
                getMessageDbValidation((DbEntityValidationException)ex);
            }
            else if (ex is DbUpdateException)
            {
                getMessageDbUpdateException((DbUpdateException)ex);
            }
            else
                getMessageException(ex);

            return View("Error");
        }

        protected void getMessageException(Exception ex)
        {
            throw new Exception(ex.Message);
        }

        protected void getMessageDbValidation(DbEntityValidationException ex)
        {
            var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            string message = String.Format("Error Detected in DataBase validation! " + fullErrorMessage);
            throw new Exception(message);
        }

        protected void getMessageDbUpdateException(DbUpdateException ex)
        {
            throw new Exception(ex.InnerException.InnerException.Message);
        }

        protected void getModelStateValidations()
        {
            string message = String.Concat("Error Detected in Validation! ", string.Join("; ", ModelState.Values
                                                    .SelectMany(x => x.Errors)
                                                    .Select(x => x.ErrorMessage)));
            throw new Exception(message);
        }

        protected void success(string message)
        {
            notifier.Success(message);
        }

        protected void info(string message)
        {
            notifier.Info(message);
        }

        protected void warning(string message)
        {
            notifier.Warning(message);
        }

        protected void danger(string message)
        {
            notifier.Error(message);
        }

        #endregion
    }
}
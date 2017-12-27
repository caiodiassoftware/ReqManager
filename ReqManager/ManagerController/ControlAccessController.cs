using DataTables.Mvc;
using ReqManager.Filters;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
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

        #endregion

        #region Constructor

        public ControlAccessController(IService<TEntity> service)
        {
            this.Service = service;
        }

        #endregion

        #region GETS

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
                List<TEntity> entities = Service.getAll().ToList();
                List<TEntity> result = new List<TEntity>();
                string searchValue = requestModel.Search.Value;

                if (!string.IsNullOrEmpty(searchValue))
                {
                    foreach (TEntity item in entities)
                    {
                        foreach (PropertyInfo pi in item.GetType().GetProperties())
                        {
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
                    result = entities.Take(5).ToList();
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
                getMessageDbValidation((DbEntityValidationException) ex);
            }
            else if (ex is DbUpdateException)
            {
                getMessageDbUpdateException((DbUpdateException) ex);
            }

            return View("Error");
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
            ModelState.Clear();
            throw new Exception(message);
        }

        protected void getModelStateValidations()
        {
            TempData["ControllerMessage"] = String.Concat("Error Detected in View validation! ", string.Join("; ", ModelState.Values
                                                    .SelectMany(x => x.Errors)
                                                    .Select(x => x.ErrorMessage)));
        }

        #endregion
    }
}
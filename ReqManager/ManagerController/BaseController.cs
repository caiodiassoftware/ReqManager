using ReqManager.Entities.Acess;
using ReqManager.Services.Estructure;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace ReqManager.ManagerController
{
    public class BaseController<TEntity> : ControlAcessController where TEntity : class
    {
        #region Attributes

        protected IService<TEntity> Service { get; set; }
        protected List<ControllerBase> viewBags { get; set; }
        #endregion

        #region Constructor

        public BaseController(IService<TEntity> service)
        {
            this.Service = service;
        }

        #endregion

        #region GETS

        public virtual ActionResult Index()
        {
            try
            {
                return View(Service.getAll());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TEntity model = Service.get(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TEntity model = Service.get(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TEntity model = Service.get(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntity entity)
        {
            try
            {
                setIdUser(ref entity);

                if (ModelState.IsValid)
                {
                    Service.add(entity);
                    Service.saveChanges();
                    ViewBag.MessageReqManager = String.Format("Register was made with Success!");
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View();
            }
            catch (DbEntityValidationException ex)
            {
                return getMessageDbValidation(entity, ex);
            }
            catch (DbUpdateException ex)
            {
                return getMessageDbUpdateException(entity, ex);
            }
            catch (Exception ex)
            {
                return getMessageGeralException(entity, ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(entity);
                    Service.saveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (DbEntityValidationException ex)
            {
                return getMessageDbValidation(entity, ex);
            }
            catch (DbUpdateException ex)
            {
                return getMessageDbUpdateException(entity, ex);
            }
            catch (Exception ex)
            {
                return getMessageGeralException(entity, ex);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Service.delete(id);
                Service.saveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MessageReqManager = String.Format("Error Detected! " + ex.Message);
                return RedirectToAction("Index");
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
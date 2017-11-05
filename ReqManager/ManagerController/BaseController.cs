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
    public class BaseController<TEntity> : ControlAcessController<TEntity> where TEntity : class
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
                    Service.add(ref entity);
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
                    Service.update(ref entity);
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
    }
}
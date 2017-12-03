using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net;
using System.Web.Mvc;

namespace ReqManager.ManagerController
{
    public class BaseController<TEntity> : ControlAcessController<TEntity> where TEntity : class
    {
        #region Attributes
        protected List<ControllerBase> viewBags { get; set; }
        #endregion

        #region Constructor

        public BaseController(IService<TEntity> service) : base (service)
        {

        }

        #endregion

        #region GETS
        

        public virtual ActionResult Index()
        {
            try
            {
                return View(Service.getAll(5));
            }
            catch (Exception ex)
            {
                TempData["ControllerMessage"] = String.Format("There was an Error while viewing logs! " + ex.Message);
                return RedirectToAction("Index");
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
                TempData["ControllerMessage"] = String.Format("Error occurred while showing Details! " + ex.Message);
                return RedirectToAction("Index");
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
                TempData["ControllerMessage"] = String.Format("Error occurred while creating! " + ex.Message);
                return RedirectToAction("Index");
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
                TempData["ControllerMessage"] = String.Format("Error occurred while editing! " + ex.Message);
                return RedirectToAction("Index");
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
                TempData["ControllerMessage"] = String.Format("Error occurred while deleting! " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region POST

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public virtual ActionResult Create(TEntity entity)
        {
            try
            {
                setIdUser(ref entity);

                if (ModelState.IsValid)
                {
                    Service.add(ref entity);
                    ModelState.Clear();
                    TempData["ControllerMessage"] = String.Format("Register was made with Success!");
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
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public virtual ActionResult Edit(TEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(ref entity);
                    TempData["ControllerMessage"] = String.Format("Registration has been successfully edited!!");
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
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Service.delete(id);
                TempData["ControllerMessage"] = String.Format("Registration has been successfully deleted!!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ControllerMessage"] = String.Format("Error Detected! " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
}
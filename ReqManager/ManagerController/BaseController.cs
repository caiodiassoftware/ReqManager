using ReqManager.Services.Estructure;
using System;
using System.Net;
using System.Web.Mvc;

namespace ReqManager.ManagerController
{
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class BaseController<TEntity> : ControlAccessController<TEntity> where TEntity : class
    {
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
                return View(Service.getAll(top));
            }
            catch (Exception ex)
            {
                return filterException(ex);
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
                return filterException(ex);
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
                return filterException(ex);
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
                return filterException(ex);
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
                return filterException(ex);
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
                setCreationDate(ref entity);

                if (ModelState.IsValid)
                {
                    Service.add(ref entity);
                    ModelState.Clear();
                    success("Register was made with Success!");
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View();
            }
            catch (Exception ex)
            {
                return filterException(ex);
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
                    success("Register has been successfully edited!");
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (Exception ex)
            {
                return filterException(ex);
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
                success("Record has been successfully deleted!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return filterException(ex);
            }
        }

        #endregion
    }
}
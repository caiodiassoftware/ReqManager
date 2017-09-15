using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.ManagerController
{
    public class BaseController<T> : Controller where T : class
    {
        #region Attributes

        private IService<T> service { get; set; }

        #endregion

        #region Constructor

        public BaseController(IService<T> service)
        {
            this.service = service;
        }

        #endregion

        #region GETS

        public ActionResult Index()
        {
            return View(service.getAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T model = service.get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T model = service.get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T model = service.get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                service.add(model);
                service.saveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BindAttribute bindAttribute)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T model = service.get(id);
            service.delete(model);
            service.saveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            base.OnActionExecuting(filterContext);
        }

        #endregion
    }
}
using ReqManager.Entities.Acess;
using ReqManager.Services.Estructure;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReqManager.ManagerController
{
    public class BaseController<T> : Controller where T : class
    {
        #region Attributes

        protected IService<T> Service { get; set; }

        #endregion

        #region Constructor

        public BaseController(IService<T> service)
        {
            this.Service = service;
        }

        #endregion

        #region GETS

        public virtual ActionResult Index()
        {
            return View(Service.getAll());
        }

        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T model = Service.get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T model = Service.get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T model = Service.get(id);
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
        public virtual ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                Service.add(model);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(T entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(entity);
                    Service.saveChanges();
                    return RedirectToAction("Index");
                }

                return View(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            T model = Service.get(id);
            Service.delete(model);
            Service.saveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods

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

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    { { "Controller", controllerName },
                        { "Action", actionName } });
                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
using ReqManager.Entities.Acess;
using ReqManager.Services.Estructure;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReqManager.ManagerController
{
    public class BaseController<TEntity> : Controller where TEntity : class
    {
        #region Attributes

        protected IService<TEntity> Service { get; set; }

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
                var test = Service.getAll();
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
        public virtual ActionResult Create(TEntity model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.add(model);
                    Service.saveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MessageReqManager = String.Format("Register was made with Success!");
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.MessageReqManager = String.Format("Error Detected! " + ex.Message);                
                return View(model);
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
            try
            {
                Service.delete(id);
                Service.saveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        #endregion
    }
}
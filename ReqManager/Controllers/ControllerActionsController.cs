using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data;
using ReqManager.Models;
using System.Reflection;
using ReqManager.Services.Acess;
using ReqManager.Services.InterfacesServices;
using ReqManager.Data.Infrastructure;

namespace ReqManager.Controllers
{
    public class ControllerActionsController : Controller
    {
        private readonly IControllerActionService service;

        public ControllerActionsController(IControllerActionService service)
        {
            this.service = service;
        }

        // GET: ControllerActions
        public ActionResult Index()
        {
            return View(service.GetAll().ToList());
        }

        public ActionResult Refresh()
        {
            try
            {
                Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));
                List<ControllerAction> controllerActionListApplication = asm.GetTypes()
                                        .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                                        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                                        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                                        .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name })
                                        .OrderBy(x => x.Controller).ThenBy(x => x.Action).Select(ca => new ControllerAction { action = ca.Action, controller = ca.Controller }).ToList();
                service.refresh(controllerActionListApplication);
                service.saveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
using ReqManager.Services.InterfacesServices;

namespace ReqManager.Controllers
{
    public class RoleControllerActionsController : Controller
    {
        private readonly IRoleControllerActionService service;

        public RoleControllerActionsController(IRoleControllerActionService service)
        {
            this.service = service;
        }

        // GET: RoleControllerActions
        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        // GET: RoleControllerActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleControllerAction roleControllerAction = service.Get(id);
            if (roleControllerAction == null)
            {
                return HttpNotFound();
            }
            return View(roleControllerAction);
        }

        // GET: RoleControllerActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleControllerActions/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_roleControllerAction,ID_role,ID_controllerAction")] RoleControllerAction roleControllerAction)
        {
            if (ModelState.IsValid)
            {
                service.add(roleControllerAction);
                service.saveChanges();
                return RedirectToAction("Index");
            }

            return View(roleControllerAction);
        }

        // GET: RoleControllerActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleControllerAction roleControllerAction = service.Get(id);
            if (roleControllerAction == null)
            {
                return HttpNotFound();
            }
            return View(roleControllerAction);
        }

        // POST: RoleControllerActions/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_roleControllerAction,ID_role,ID_controllerAction")] RoleControllerAction roleControllerAction)
        {
            if (ModelState.IsValid)
            {
                service.edit(roleControllerAction);
                service.saveChanges();
                return RedirectToAction("Index");
            }
            return View(roleControllerAction);
        }

        // GET: RoleControllerActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleControllerAction roleControllerAction = service.Get(id);
            if (roleControllerAction == null)
            {
                return HttpNotFound();
            }
            return View(roleControllerAction);
        }

        // POST: RoleControllerActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoleControllerAction roleControllerAction = service.Get(id);
            service.delete(roleControllerAction);
            service.saveChanges();
            return RedirectToAction("Index");
        }
    }
}

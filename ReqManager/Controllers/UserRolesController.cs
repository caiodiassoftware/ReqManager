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
    public class UserRolesController : Controller
    {
        private readonly IUserRoleService service;

        public UserRolesController(IUserRoleService service)
        {
            this.service = service;
        }

        // GET: UserRoles
        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = service.Get(Convert.ToInt32(id));
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_userRole,ID_user,ID_role")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                service.add(userRole);
                service.saveChanges();
                return RedirectToAction("Index");
            }

            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = service.Get(Convert.ToInt32(id));
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_userRole,ID_user,ID_role")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                service.edit(userRole);
                service.saveChanges();
                return RedirectToAction("Index");
            }
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = service.Get(Convert.ToInt32(id));
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole userRole = service.Get(Convert.ToInt32(id));
            service.delete(userRole);
            service.saveChanges();
            return RedirectToAction("Index");
        }
    }
}

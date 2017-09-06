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

namespace ReqManager.Controllers
{
    public class RoleControllerActionsController : Controller
    {
        private ReqManagerEntities db = new ReqManagerEntities();

        // GET: RoleControllerActions
        public ActionResult Index()
        {
            return View(db.RoleControllerActions.ToList());
        }

        // GET: RoleControllerActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleControllerAction roleControllerAction = db.RoleControllerActions.Find(id);
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
                db.RoleControllerActions.Add(roleControllerAction);
                db.SaveChanges();
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
            RoleControllerAction roleControllerAction = db.RoleControllerActions.Find(id);
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
                db.Entry(roleControllerAction).State = EntityState.Modified;
                db.SaveChanges();
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
            RoleControllerAction roleControllerAction = db.RoleControllerActions.Find(id);
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
            RoleControllerAction roleControllerAction = db.RoleControllerActions.Find(id);
            db.RoleControllerActions.Remove(roleControllerAction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

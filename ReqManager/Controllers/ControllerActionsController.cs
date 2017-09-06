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
    public class ControllerActionsController : Controller
    {
        private ReqManagerEntities db = new ReqManagerEntities();

        // GET: ControllerActions
        public ActionResult Index()
        {
            return View(db.ControllerActions.ToList());
        }

        // GET: ControllerActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControllerAction controllerAction = db.ControllerActions.Find(id);
            if (controllerAction == null)
            {
                return HttpNotFound();
            }
            return View(controllerAction);
        }

        // GET: ControllerActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ControllerActions/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_controllerAction,controller,action")] ControllerAction controllerAction)
        {
            if (ModelState.IsValid)
            {
                db.ControllerActions.Add(controllerAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(controllerAction);
        }

        // GET: ControllerActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControllerAction controllerAction = db.ControllerActions.Find(id);
            if (controllerAction == null)
            {
                return HttpNotFound();
            }
            return View(controllerAction);
        }

        // POST: ControllerActions/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_controllerAction,controller,action")] ControllerAction controllerAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controllerAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(controllerAction);
        }

        // GET: ControllerActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControllerAction controllerAction = db.ControllerActions.Find(id);
            if (controllerAction == null)
            {
                return HttpNotFound();
            }
            return View(controllerAction);
        }

        // POST: ControllerActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControllerAction controllerAction = db.ControllerActions.Find(id);
            db.ControllerActions.Remove(controllerAction);
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

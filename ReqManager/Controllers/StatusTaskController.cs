using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Model;
using ReqManager.Services.Task.Interfaces;

namespace ReqManager.Controllers
{
    public class StatusTaskController : Controller
    {
        private IStatusTaskService service { get; set; }
        private ReqManagerEntities db = new ReqManagerEntities();

        public StatusTaskController(IStatusTaskService service)
        {
            this.service = service;
        }

        // GET: StatusTask
        public ActionResult Index()
        {
            return View(service.getAll());
        }

        // GET: StatusTask/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STATUS_TASK sTATUS_TASK = db.STATUS_TASK.Find(id);
            if (sTATUS_TASK == null)
            {
                return HttpNotFound();
            }
            return View(sTATUS_TASK);
        }

        // GET: StatusTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskStatusID,description")] STATUS_TASK sTATUS_TASK)
        {
            if (ModelState.IsValid)
            {
                db.STATUS_TASK.Add(sTATUS_TASK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTATUS_TASK);
        }

        // GET: StatusTask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STATUS_TASK sTATUS_TASK = db.STATUS_TASK.Find(id);
            if (sTATUS_TASK == null)
            {
                return HttpNotFound();
            }
            return View(sTATUS_TASK);
        }

        // POST: StatusTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskStatusID,description")] STATUS_TASK sTATUS_TASK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTATUS_TASK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTATUS_TASK);
        }

        // GET: StatusTask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STATUS_TASK sTATUS_TASK = db.STATUS_TASK.Find(id);
            if (sTATUS_TASK == null)
            {
                return HttpNotFound();
            }
            return View(sTATUS_TASK);
        }

        // POST: StatusTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STATUS_TASK sTATUS_TASK = db.STATUS_TASK.Find(id);
            db.STATUS_TASK.Remove(sTATUS_TASK);
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

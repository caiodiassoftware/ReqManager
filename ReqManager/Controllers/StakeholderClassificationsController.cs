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

namespace ReqManager.Controllers
{
    public class StakeholderClassificationsController : Controller
    {
        private ReqManagerEntities db = new ReqManagerEntities();

        // GET: StakeholderClassifications
        public ActionResult Index()
        {
            return View(db.StakeholderClassification.ToList());
        }

        // GET: StakeholderClassifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StakeholderClassification stakeholderClassification = db.StakeholderClassification.Find(id);
            if (stakeholderClassification == null)
            {
                return HttpNotFound();
            }
            return View(stakeholderClassification);
        }

        // GET: StakeholderClassifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StakeholderClassifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassificationID,description")] StakeholderClassification stakeholderClassification)
        {
            if (ModelState.IsValid)
            {
                db.StakeholderClassification.Add(stakeholderClassification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stakeholderClassification);
        }

        // GET: StakeholderClassifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StakeholderClassification stakeholderClassification = db.StakeholderClassification.Find(id);
            if (stakeholderClassification == null)
            {
                return HttpNotFound();
            }
            return View(stakeholderClassification);
        }

        // POST: StakeholderClassifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassificationID,description")] StakeholderClassification stakeholderClassification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stakeholderClassification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stakeholderClassification);
        }

        // GET: StakeholderClassifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StakeholderClassification stakeholderClassification = db.StakeholderClassification.Find(id);
            if (stakeholderClassification == null)
            {
                return HttpNotFound();
            }
            return View(stakeholderClassification);
        }

        // POST: StakeholderClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StakeholderClassification stakeholderClassification = db.StakeholderClassification.Find(id);
            db.StakeholderClassification.Remove(stakeholderClassification);
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

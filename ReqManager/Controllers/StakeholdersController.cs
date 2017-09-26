using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Views
{
    public class StakeholdersController : BaseController<StakeholdersEntity>
    {
        private IStakeholderClassificationService classService { get; set; }
        private IUserService userService { get; set; }
        private IStakeholdersService service { get; set; }

        public StakeholdersController(IStakeholdersService service, IStakeholderClassificationService classService, IUserService userService) : base(service)
        {
            this.classService = classService;
            this.userService = userService;
            this.service = service;
        }

        public override ActionResult Create()
        {
            dropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([Bind(Include = "ClassificationID,UserID")] StakeholdersEntity entity)
        {
            if (ModelState.IsValid)
            {
                Service.add(entity);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            dropDowns();
            return View(entity);
        }


        public override ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StakeholdersEntity entity = Service.get(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            dropDowns();
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit([Bind(Include = "UserID, ClassificationID")] StakeholdersEntity entity)
        {
            if (ModelState.IsValid)
            {
                Service.update(entity);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            dropDowns();
            return View(entity);
        }

        private void dropDowns()
        {
            List<StakeholdersEntity> list = Service.getAll().ToList();
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            ViewBag.ClassificationID = new SelectList(classService.getAll(), "ClassificationID", "description");
        }
    }
}

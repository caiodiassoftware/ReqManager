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
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class ProjectRequirementsController : BaseController<ProjectRequirementsEntity>
    {
        private IProjectRequirementsService service { get; set; }
        private IProjectService projectService { get; set; }
        private IUserService userService { get; set; }
        private IProjectRequirementService reqService { get; set; }

        public ProjectRequirementsController(
            IProjectRequirementsService service,
            IProjectService projectService,
            IUserService userService,
            IProjectRequirementService reqService)
            : base(service)
        {
            this.service = service;
            this.projectService = projectService;
            this.userService = userService;
            this.reqService = reqService;
        }

        public override ActionResult Create()
        {
            return dropDowns();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([Bind(Include = "ProjectRequirementID,UserID,ProjectID,RequirementID,creationDate,traceable")] ProjectRequirementsEntity projectRequirementsEntity)
        {
            base.Create(projectRequirementsEntity);
            return dropDowns(projectRequirementsEntity);
        }

        public override ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectRequirementsEntity projectRequirementsEntity = Service.get(id);
            if (projectRequirementsEntity == null)
            {
                return HttpNotFound();
            }
            return dropDowns(projectRequirementsEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit([Bind(Include = "ProjectRequirementID,UserID,ProjectID,RequirementID,creationDate,traceable")] ProjectRequirementsEntity projectRequirementsEntity)
        {
            base.Edit(projectRequirementsEntity);
            return dropDowns(projectRequirementsEntity);
        }

        public override ActionResult Delete(int? id)
        {
            base.Delete(id);
            return dropDowns(Service.get(id));
        }

        private ActionResult dropDowns(ProjectRequirementsEntity entity = null)
        {
            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            ViewBag.RequirementID = new SelectList(reqService.getAll(), "RequirementID", "code");
            return entity == null ? View() : View(entity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class ProjectArtifactController : BaseController<ProjectArtifactEntity>
    {
        private IProjectArtifactService service { get; set; }
        private IUserService userService { get; set; }
        private IMeasureImportanceService measureService { get; set; }
        private IProjectService projectService { get; set; }

        public ProjectArtifactController(
            IProjectArtifactService service,
            IUserService userService,
            IMeasureImportanceService measureService,
            IProjectService projectService) : base(service)
        {
            this.service = service;
            this.userService = userService;
            this.measureService = measureService;
            this.projectService = projectService;
        }

        #region GETS

        public override ActionResult Create()
        {
            return dropDowns();
        }

        public override ActionResult Edit(int? id)
        {
            base.Edit(id);
            return dropDowns(Service.get(id));
        }

        public override ActionResult Delete(int? id)
        {
            base.Delete(id);
            return dropDowns(Service.get(id));
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(ProjectArtifactEntity ProjectArtifactEntity)
        {
            base.Create(ProjectArtifactEntity);
            return dropDowns(ProjectArtifactEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(ProjectArtifactEntity ProjectArtifactEntity)
        {
            base.Edit(ProjectArtifactEntity);
            return dropDowns(ProjectArtifactEntity);
        }

        #endregion

        #region Private Methods

        private ActionResult dropDowns(ProjectArtifactEntity entity = null)
        {
            ViewBag.MeasureImportanceID = new SelectList(measureService.getAll(), "MeasureImportanceID", "description");
            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}

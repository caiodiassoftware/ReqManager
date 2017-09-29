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

namespace ReqManager.Controllers
{
    public class ProjectsController : BaseController<ProjectEntity>
    {
        private IProjectService service { get; set; }
        private IUserService userService { get; set; }
        private IProjectPhasesService phasesService { get; set; }

        public ProjectsController(
            IProjectService service,
            IUserService userService,
            IProjectPhasesService phasesService) : base(service)
        {
            this.service = service;
            this.userService = userService;
            this.phasesService = phasesService;
        }

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

        private ActionResult dropDowns(ProjectEntity entity = null)
        {
            ViewBag.ProjectPhasesID = new SelectList(phasesService.getAll(), "ProjectPhasesID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }
    }
}

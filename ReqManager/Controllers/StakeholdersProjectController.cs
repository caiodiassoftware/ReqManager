using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace ReqManager.Controllers
{
    public class StakeholdersProjectController : BaseController<StakeholdersProjectEntity>
    {
        private IStakeholdersProjectService service { get; set; }

        public StakeholdersProjectController(
            IStakeholdersProjectService service,
            IProjectService projectService,
            IStakeholdersService stakeholderService) : base(service)
        {
            this.service = service;
            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "description");
            ViewBag.StakeholderID = new SelectList(stakeholderService.getAll(), "StakeholderID", "DisplayName");
        }

        [HttpPost, ActionName("EditImportance")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int StakeholdersProjectID, int importanceValue)
        {
            StakeholdersProjectEntity entity = Service.get(StakeholdersProjectID);
            entity.importanceValue = importanceValue;
            Service.update(ref entity);
            return View("Index");
        }

        [HttpPost]
        public JsonResult Add(int StakeholderID, int ProjectID, string description, int importanceValue)
        {
            bool success = false;
            string message = string.Empty;

            try
            {
                StakeholdersProjectEntity stakeholderProject = new StakeholdersProjectEntity();
                stakeholderProject.ProjectID = ProjectID;
                stakeholderProject.StakeholderID = StakeholderID;
                stakeholderProject.description = description;
                stakeholderProject.importanceValue = importanceValue;
                setCreationDate(ref stakeholderProject);
                setIdUser(ref stakeholderProject);
                if (TryValidateModel(stakeholderProject))
                {
                    Service.add(ref stakeholderProject);
                    success = true;
                    message = "Register was mage with Success!";
                }
                else
                {
                    message = "Please enter all the fields!";
                }
            }
            catch (DbUpdateException)
            {
                message = "This stakeholder is already linked to this project!";
            }

            return Json(new { success, message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStakeholdersFromProject(int ProjectID)
        {
            try
            {
                return Json(service.getStakeholderByProject(ProjectID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

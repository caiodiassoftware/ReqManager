using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Linq;

namespace ReqManager.Controllers
{
    public class StakeholdersProjectController : BaseController<StakeholdersProjectEntity>
    {
        public StakeholdersProjectController(
            IStakeholdersProjectService service,
            IProjectService projectService,
            IStakeholdersService stakeholderService) : base(service)
        {
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
        public void Add(int StakeholderID, int ProjectID, string description, int importanceValue)
        {
            try
            {
                StakeholdersProjectEntity stakeholderProject = new StakeholdersProjectEntity();
                stakeholderProject.ProjectID = ProjectID;
                stakeholderProject.StakeholderID = StakeholderID;
                stakeholderProject.description = description;
                stakeholderProject.importanceValue = importanceValue;
                setCreationDate(ref stakeholderProject);
                setIdUser(ref stakeholderProject);
                if(TryValidateModel(stakeholderProject))
                {
                    Service.add(ref stakeholderProject);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetStakeholdersFromProject(int ProjectID)
        {
            try
            {
                return Json(Service.filter(s => s.ProjectID == ProjectID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

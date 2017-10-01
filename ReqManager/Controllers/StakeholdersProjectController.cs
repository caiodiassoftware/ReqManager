using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

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
    }
}

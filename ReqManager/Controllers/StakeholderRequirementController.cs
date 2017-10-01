using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementEntity>
    {
        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IStakeholdersService stakeholderService,
            IProjectRequirementsService projectReqService) : base(service)
        {
            ViewBag.ProjectRequirementID = new SelectList(projectReqService.getAll(), "ProjectRequirementID", "DisplayName");
            ViewBag.StakeHolderID = new SelectList(stakeholderService.getAll(), "StakeholderID", "DisplayName");
        }

    }
}

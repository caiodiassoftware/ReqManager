using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementEntity>
    {
        private IRequirementService requirementService { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }

        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IRequirementService requirementService,
            IStakeholdersProjectService stakeholderProjectService) : base(service)
        {
            this.requirementService = requirementService;
            this.stakeholderProjectService = stakeholderProjectService;

            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("RequirementID", new SelectList(requirementService.getAll(), "RequirementID", "DisplayName"));
        }

        public override ActionResult Create(StakeholderRequirementEntity entity)
        {
            base.Create(entity);
            return RedirectToAction("Details", "Requirement", new { @id = entity.RequirementID});
        }
    }
}

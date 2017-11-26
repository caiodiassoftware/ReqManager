using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Linq;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementEntity>
    {
        private IProjectRequirementsService projectRequirementService { get; set; }

        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IProjectRequirementsService projectRequirementService,
            IProjectService projectService) : base(service)
        {
            this.projectRequirementService = projectRequirementService;

            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "DisplayName");
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateNewRelationship(int ProjectID, int RequirementID, int StakeHolderID)
        {
            try
            {
                ProjectRequirementsEntity projectReq = projectRequirementService.getRequirementsByProjectAndRequirement(ProjectID, RequirementID);
                StakeholderRequirementEntity entity = new StakeholderRequirementEntity();
                entity.ProjectRequirementID = projectReq.ProjectRequirementID;
                entity.StakeHolderID = StakeHolderID;
                return base.Create(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

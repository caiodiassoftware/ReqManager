using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System.Web.Mvc;

namespace ReqManager.Controllers
{

    public class RequirementRequestForChangesController : BaseController<RequirementRequestForChangesEntity>
    {
        public RequirementRequestForChangesController(
            IRequirementRequestForChangesService service,
            IStakeholderRequirementService stakeholders,
            IRequestStatusService status,
            IRequirementService requirement) : base(service)
        {
            ViewData.Add("StakeHolderRequirementID", new SelectList(stakeholders.getAll(), "StakeHolderRequirementID", "DisplayName"));
            ViewData.Add("RequirementID", new SelectList(requirement.getAll(), "RequirementID", "DisplayName"));
            ViewData.Add("RequestStatusID", new SelectList(status.getAll(), "RequestStatusID", "description"));
        }

        public override ActionResult Create(RequirementRequestForChangesEntity entity)
        {
            entity.RequestStatusID = 1;
            return base.Create(entity);
        }
    }

}

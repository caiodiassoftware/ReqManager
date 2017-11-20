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
            IRequirementService requirement) : base(service)
        {
            ViewData.Add("StakeHolderRequirementID", new SelectList(stakeholders.getAll(), "StakeHolderRequirementID", "DisplayName"));
            ViewData.Add("RequirementID", new SelectList(requirement.getAll(), "RequirementID", "DisplayName"));
        }
    }

}

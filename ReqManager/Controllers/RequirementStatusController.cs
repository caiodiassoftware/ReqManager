using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementStatusController : BaseController<RequirementStatusEntity>
    {
        public RequirementStatusController(IRequirementStatusService service) : base(service)
        {
        }
    }
}

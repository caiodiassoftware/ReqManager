using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementTypesController : BaseController<RequirementTypeEntity>
    {
        public RequirementTypesController(IRequirementTypeService typeService) : base(typeService)
        {
        }
    }
}

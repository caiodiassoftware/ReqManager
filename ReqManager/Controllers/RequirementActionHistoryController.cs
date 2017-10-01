using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementActionHistoryController : BaseController<RequirementActionHistoryEntity>
    {
        public RequirementActionHistoryController(IRequirementActionHistoryService service, IRequirementService reqService) : base(service)
        {
            ViewData.Add("RequirementID", new SelectList(reqService.getAll(), "RequirementID", "code"));
        }
    }
}

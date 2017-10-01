using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementRationaleController : BaseController<RequirementRationaleEntity>
    {
        public RequirementRationaleController(
            IRequirementRationaleService service,
            IRequirementTypeService typeService,
            IUserService userService,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementService reqService,
            IStakeholdersProjectService stakeholderProjectService) : base(service)
        {
            ViewData.Add("RequirementID", new SelectList(reqService.getAll(), "RequirementID", "code"));
            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("MeasureImportanceID", new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description"));
            ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

    }
}

using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RequirementCharacteristicsController : BaseController<RequirementCharacteristicsEntity>
    {
        public RequirementCharacteristicsController(
            IRequirementCharacteristicsService service,
            IRequirementService requirement,
            ICharacteristicsService characteristics) : base(service)
        {
            ViewData.Add("RequirementID", new SelectList(requirement.getAll(), "RequirementID", "DisplayName"));
            ViewData.Add("CharacteristicsID", new SelectList(characteristics.getAll(), "CharacteristicsID", "name"));
        }
    }
}

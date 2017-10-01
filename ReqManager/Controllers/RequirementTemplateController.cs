using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementTemplateController : BaseController<RequirementTemplateEntity>
    {
        public RequirementTemplateController(IRequirementTemplateService service, IUserService userService) : base(service)
        {
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }
    }
}

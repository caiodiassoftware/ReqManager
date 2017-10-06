using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Web;
using ReqManager.ViewModels;

namespace ReqManager.Controllers
{
    public class RequirementTemplateController : BaseController<RequirementTemplateEntity>
    {
        public RequirementTemplateController(IRequirementTemplateService service, IUserService userService, IRequirementTypeService type) : base(service)
        {
            ViewData.Add("RequirementTypeID", new SelectList(type.getAll(), "RequirementTypeID", "description"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(RequirementTemplateEntity entity)
        {
            entity.templateHtml = HttpUtility.HtmlEncode(entity.templateHtml);
            return base.Create(entity);
        }
    }
}

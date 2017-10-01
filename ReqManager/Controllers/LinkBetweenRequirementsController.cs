using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            ITypeLinkService typeLinkService,
            IUserService userService) : base(linkService)
        {
            ViewData.Add("RequirementOriginID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("RequirementTargetID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }
    }
}

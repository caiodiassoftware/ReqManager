using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.ViewModels;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        private IRequirementTraceabilityMatrixService matrixService {get;set;}

        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            ITypeLinkService typeLinkService,
            IUserService userService,
            IRequirementTraceabilityMatrixService matrixService) : base(linkService)
        {
            this.matrixService = matrixService;

            ViewData.Add("RequirementOriginID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("RequirementTargetID", new SelectList(requirementService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeLinkService.getAll(), "TypeLinkID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public ActionResult RequirementTraceabilityMatrix()
        {
            DataTableViewModel dt = new DataTableViewModel();
            dt.dataTable = matrixService.getMatrix();
            return View(dt);
        }

        public void ShowLink(string origin, string target)
        {

        }
    }
}

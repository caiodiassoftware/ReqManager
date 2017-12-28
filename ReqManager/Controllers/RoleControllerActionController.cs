using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RoleControllerActionController : BaseController<RoleControllerActionEntity>
    {
        public RoleControllerActionController(
            IRoleControllerActionService service,
            IControllerActionService actions,
            IRoleService role) : base(service)
        {
            ViewData.Add("ControllerActionID", new SelectList(actions.getAll(), "ControllerActionID", "DisplayName"));
            ViewData.Add("RoleID", new SelectList(role.getAll(), "RoleID", "description"));
        }

        public override ActionResult Index()
        {
            return View();
        }
    }
}

using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RoleControllerActionController : BaseController<RoleControllerActionEntity>
    {
        public RoleControllerActionController(IRoleControllerActionService service, IControllerActionService caService, IRoleService roleService) : base(service)
        {

        }

        public override ActionResult Index()
        {
            return View();
        }
    }
}

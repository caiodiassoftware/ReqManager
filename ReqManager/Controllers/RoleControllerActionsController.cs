using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.ManagerController;

namespace ReqManager.Controllers
{
    public class RoleControllerActionsController : BaseController<RoleControllerAction>
    {
        public RoleControllerActionsController(IRoleControllerActionService service) : base(service)
        {

        }
    }
}

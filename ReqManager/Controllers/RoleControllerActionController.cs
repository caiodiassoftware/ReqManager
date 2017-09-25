
using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RoleControllerActionController : BaseController<RoleControllerActionEntity>
    {
        public RoleControllerActionController(IRoleControllerActionService service) : base(service)
        {

        }    
    }
}

using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Notifications.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RoleController : BaseController<RoleEntity>
    {
        public RoleController(IRoleService service, INotifierService notifier) : base(service)
        {
            
        }
    }
}

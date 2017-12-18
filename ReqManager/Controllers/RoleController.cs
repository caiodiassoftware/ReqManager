using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RoleController : BaseController<RoleEntity>
    {
        public RoleController(IRoleService service) : base(service)
        {
            
        }
    }
}

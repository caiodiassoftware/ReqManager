using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RolesController : BaseController<Entities.Acess.RoleEntity>
    {
        public RolesController(IRoleService service) : base(service)
        {

        }
    }
}

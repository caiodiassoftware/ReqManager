using ReqManager.Model;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RolesController : BaseController<Role>
    {
        public RolesController(IRoleService service) : base(service)
        {

        }
    }
}

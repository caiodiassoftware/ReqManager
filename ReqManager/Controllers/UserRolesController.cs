using ReqManager.Model;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class UserRolesController : BaseController<UserRole>
    {
        public UserRolesController(IUserRoleService service) : base(service)
        {
        }
    }
}

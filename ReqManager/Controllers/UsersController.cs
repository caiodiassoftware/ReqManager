using ReqManager.Model;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class UsersController : BaseController<Users>
    {
        public UsersController(IUserService service) : base(service)
        {
        }
    }
}

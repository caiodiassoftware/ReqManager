using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class UserController : BaseController<UserEntity>
    {
        public UserController(IUserService service) : base(service)
        {

        }
    }
}

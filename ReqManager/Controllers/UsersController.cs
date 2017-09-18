using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Entities.Acess;

namespace ReqManager.Controllers
{
    public class UsersController : BaseController<UserEntity>
    {
        public UsersController(IUserService service) : base(service)
        {
        }
    }
}

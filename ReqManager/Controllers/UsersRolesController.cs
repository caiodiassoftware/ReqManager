using System.Web.Mvc;
using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class UsersRolesController : BaseController<UserRoleEntity>
    {
        public UsersRolesController(IUserRoleService service, IRoleService roleService, IUserService userService) : base(service)
        {
            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "DisplayName");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "DisplayName");
        }
    }
}

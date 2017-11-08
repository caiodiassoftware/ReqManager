using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using System;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class UserController : BaseController<UserEntity>
    {
        public UserController(IUserService service) : base(service)
        {

        }

        public override ActionResult Create(UserEntity entity)
        {
            entity.active = true;
            return base.Create(entity);
        }
    }
}

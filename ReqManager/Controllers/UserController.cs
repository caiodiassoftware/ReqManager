using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
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

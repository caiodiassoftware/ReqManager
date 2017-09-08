using ReqManager.Services.Acess;
using ReqManager.Services.InterfacesServices;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "login,senha")] LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var user = Mapper.Map<User>(model);
                    //User user = userService.Get(model.login).FirstOrDefault();
                    //if (!user.Equals(null))
                    //{
                    //    Session["user"] = user;
                    //}
                }

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
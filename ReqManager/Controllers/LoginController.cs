using ReqManager.Entities.Acess;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using ReqManager.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using ReqManager.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Controllers
{
    public class LoginController : Controller
    {
        private IUserService userService;
        private IControllerActionService caService;

        public LoginController(IUserService userService,
            IControllerActionService caService)
        {
            this.userService = userService;
            this.caService = caService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public void Login([Bind(Include = "login,password")] LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserEntity user = userService.Login(model.login, model.password);

                    if (user != null)
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                1,
                                model.login,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(30),
                                true,
                                user.UserID.ToString(),
                                FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        Response.Redirect(@"~/Role/Index", false);
                    }
                    else
                    {
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
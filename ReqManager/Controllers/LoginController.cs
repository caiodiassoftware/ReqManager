using ReqManager.Entities.Acess;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using ReqManager.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using ReqManager.Utils.Hashing;
using ReqManager.Filters;

namespace ReqManager.Controllers
{
    public class LoginController : Controller
    {
        private IUserService userService { get; set; }
        private IControllerActionService caService { get; set; }

        public LoginController(
            IUserService userService,
            IControllerActionService caService)
        {
            this.caService = caService;
            this.userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public void Login([Bind(Include = "login, password")] LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserEntity user = userService.Get(model.login);

                    if (user != null)
                    {
                        var hashCode = user.verificationCode;
                        var encodingPasswordString = CryptographySHA1.EncodePassword(model.password, hashCode);

                        if (user.password.Equals(encodingPasswordString))
                        {
                            Session["name"] = user.nickName;
                            Session["permissions"] = caService.GetPermissions(user.UserID);

                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                    1,
                                    model.login,
                                    DateTime.Now,
                                    DateTime.Now.AddHours(1),
                                    true,
                                    user.UserID.ToString(),
                                    FormsAuthentication.FormsCookiePath);

                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            cookie.HttpOnly = true;
                            cookie.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(cookie);

                            Response.Redirect(@"~/Projects/Index", false);
                        }
                        else
                        {
                            Response.Redirect(@"~/Login/Login", false);
                            ModelState.AddModelError(string.Empty, "Login or Password are Incorrect!");
                        }
                    }
                    else
                    {
                        Response.Redirect(@"~/Login/Login", false);
                        ModelState.AddModelError(string.Empty, "Login or Password are Incorrect!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void LogOut()
        {
            try
            {
                HttpCookie authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                FormsAuthentication.SignOut();
                authCookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Response.Cookies.Add(authCookie);

                Response.Redirect("~/Login/Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void ResetPassword(string loginReset, string documentReset, string newPassword)
        {
            try
            {
                userService.ResetPassword(loginReset, documentReset, newPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
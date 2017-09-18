using ReqManager.Services.Acess.Interfaces;
using ReqManager.ViewModels;
using System;
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
        public void Login([Bind(Include = "login,senha")] LoginViewModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //    List<ControllerActionViewModel> list = new List<ControllerActionViewModel>();
                //    Users user = userService.Login(model.login, model.senha);
                //    if (user != null)
                //    {
                //        Session["user"] = user;

                //        var roles = user.UserRole.Select(ur => ur.Role);
                //        var rca = roles.Select(r => r.RoleControllerAction);
                //        var ca = rca.Select(x => x.Select( xa => xa.ControllerAction));
                //        var teste = ca.Select(la => la.Select(l =>
                //            new ControllerActionViewModel
                //            {
                //                Action = l.action,
                //                Controller = l.controller,
                //                IsGet = l.IsGet,
                //                ControllerActionID = l.ControllerActionID                                
                //            }
                //        ));

                //        foreach (var item in teste)                        
                //            list.AddRange(item);

                //        Session["controllerActions"] = list;
                //        Response.Redirect(@"~/Users/Index", false);
                //    }                        
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
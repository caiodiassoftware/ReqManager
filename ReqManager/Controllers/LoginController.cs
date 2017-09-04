using ReqManager.Services.Acess;
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

                    //Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

                    //var controlleractionlist = asm.GetTypes()
                    //        .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    //        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    //        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    //        .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    //        .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

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
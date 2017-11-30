using ReqManager.Entities.Acess;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace ReqManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        private readonly IControllerActionService caService;
        private readonly IRoleControllerActionService rcaService;
        private readonly IRoleService roleService;
        private readonly IUserRoleService urService;

        public LoginController(IUserService userService, 
            IControllerActionService caService, 
            IRoleControllerActionService rcaService,
            IRoleService roleService,
            IUserRoleService urService)
        {
            this.userService = userService;
            this.caService = caService;
            this.rcaService = rcaService;
            this.roleService = roleService;
            this.urService = urService;
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
                if (ModelState.IsValid)
                {
                    List<ControllerActionViewModel> list = new List<ControllerActionViewModel>();
                    UserEntity user = userService.Login(model.login, model.password);
                    if (user != null)
                    {
                        Session["user"] = user;

                        var roles = roleService.getAll().ToList();
                        var rcas = rcaService.getAll().ToList();
                        var cas = caService.getAll().ToList();
                        var userroles = urService.getAll().ToList();

                        var controllerActions = from ur in userroles
                                                join role in roles on ur.RoleID equals role.RoleID
                                                join rca in rcas on role.RoleID equals rca.RoleID
                                                join ca in cas on rca.ControllerActionID equals ca.ControllerActionID
                                                where ur.UserID == user.UserID
                                                select new ControllerActionViewModel
                                                {
                                                    Action = ca.action,
                                                    Controller = ca.controller,
                                                    IsGet = ca.IsGet,
                                                    ControllerActionID = ca.ControllerActionID
                                                };

                        foreach (var item in controllerActions.ToList())
                            list.Add(item);

                        Session["controllerActions"] = list;
                        Response.Redirect(@"~/Users/Index", false);
                    }
                    else
                    {
                        Response.Redirect(@"~/Login/Login", false);
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
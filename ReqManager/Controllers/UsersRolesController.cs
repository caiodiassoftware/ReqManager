using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Task;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.ViewModels;

namespace ReqManager.Controllers
{
    public class UsersRolesController : BaseController<UserRoleEntity>
    {
        private IRoleService roleService { get; set; }
        private IUserService userService { get; set; }

        public UsersRolesController(IUserRoleService service, IRoleService roleService, IUserService userService) : base(service)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        public override ActionResult Index()
        {
            List<UserRoleViewModel> view = new List<UserRoleViewModel>();

            foreach (var item in Service.getAll().ToList())
            {
                UserRoleViewModel ur = new UserRoleViewModel();
                ur.UserRoleID = item.UserRoleID;
                ur.roleDescription = roleService.get(item.RoleID).description;
                ur.userName = userService.get(item.UserID).name;
                view.Add(ur);
            }

            return View(view);
        }

        public override ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([Bind(Include = "UserRoleID,RoleID,UserID")] UserRoleEntity userRoleEntity)
        {
            if (ModelState.IsValid)
            {
                Service.add(userRoleEntity);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return View(userRoleEntity);
        }


        public override ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserRoleEntity userRoleEntity = Service.get(id);

            if (userRoleEntity == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return View(userRoleEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit([Bind(Include = "UserRoleID,RoleID,UserID")] UserRoleEntity userRoleEntity)
        {
            if (ModelState.IsValid)
            {
                Service.update(userRoleEntity);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return View(userRoleEntity);
        }
    }
}

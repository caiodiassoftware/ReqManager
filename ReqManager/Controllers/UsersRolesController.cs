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

        public override ActionResult Create()
        {
            var teste = roleService.getAll();

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

            ViewBag.RoleID = new SelectList(Service.getAll().Select(r => r.Role), "RoleID", "description");
            ViewBag.UserID = new SelectList(Service.getAll().Select(u => u.User), "UserID", "name");
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

            ViewBag.RoleID = new SelectList(Service.getAll().Select(r => r.Role), "RoleID", "description");
            ViewBag.UserID = new SelectList(Service.getAll().Select(u => u.User), "UserID", "name");
            return View(userRoleEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleID,RoleID,UserID")] UserRoleEntity userRoleEntity)
        {
            if (ModelState.IsValid)
            {
                Service.update(userRoleEntity);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(Service.getAll().Select(r => r.Role), "RoleID", "description");
            ViewBag.UserID = new SelectList(Service.getAll().Select(u => u.User), "UserID", "name");
            return View(userRoleEntity);
        }
    }
}

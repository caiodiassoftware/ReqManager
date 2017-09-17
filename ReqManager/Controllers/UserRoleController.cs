using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Model;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class UserRoleController : BaseController<UserRole>
    {
        public UserRoleController(IUserRoleService service) : base(service)
        {

        }

        public override ActionResult Index()
        {
            return View(Service.getAll());
        }

        public override ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(((IUserRoleService)Service).GetRoles(), "RoleID", "description");
            ViewBag.UserID = new SelectList(((IUserRoleService)Service).GetUsers(), "UserID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([Bind(Include = "UserRoleID,RoleID,UserID")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                Service.add(userRole);
                Service.saveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(((IUserRoleService)Service).GetRoles(), "RoleID", "description");
            ViewBag.UserID = new SelectList(((IUserRoleService)Service).GetUsers(), "UserID", "name");
            return View(userRole);
        }

        
    }
}

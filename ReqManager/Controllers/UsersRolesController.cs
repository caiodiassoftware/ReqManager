﻿using System;
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

        #region GETS

        public override ActionResult Create()
        {
            return dropDowns();
        }

        public override ActionResult Edit(int? id)
        {
            base.Edit(id);
            return dropDowns(Service.get(id));
        }

        public override ActionResult Delete(int? id)
        {
            base.Delete(id);
            return dropDowns(Service.get(id));
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(UserRoleEntity UserRoleEntity)
        {
            base.Create(UserRoleEntity);
            return dropDowns(UserRoleEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(UserRoleEntity UserRoleEntity)
        {
            base.Edit(UserRoleEntity);
            return dropDowns(UserRoleEntity);
        }

        #endregion

        #region Private Methods

        private ActionResult dropDowns(UserRoleEntity entity = null)
        {
            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }

        #endregion


        //public override ActionResult Index()
        //{
        //    List<UserRoleViewModel> view = new List<UserRoleViewModel>();

        //    foreach (var item in Service.getAll().ToList())
        //    {
        //        UserRoleViewModel ur = new UserRoleViewModel();
        //        ur.UserRoleID = item.UserRoleID;
        //        ur.roleDescription = roleService.get(item.RoleID).description;
        //        ur.userName = userService.get(item.UserID).name;
        //        view.Add(ur);
        //    }

        //    return View(view);
        //}
    }
}

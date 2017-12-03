using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Services.InterfacesServices;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using ReqManager.Model;
using System.Linq;
using ReqManager.Entities;
using AutoMapper;
using ReqManager.Services.Extensions;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Entities.Acess;

namespace ReqManager.Services.Acess
{
    public class ControllerActionService : ServiceBase<ControllerAction, ControllerActionEntity>, IControllerActionService
    {
        private readonly IRoleControllerActionService roleControllerActionService;
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;

        public ControllerActionService(
            IControllerActionRepository repository,
            IRoleControllerActionService roleControllerActionService,
            IRoleService roleService,
            IUserRoleService userRoleService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.roleControllerActionService = roleControllerActionService;
            this.roleService = roleService;
            this.userRoleService = userRoleService;
        }


        public void Refresh(IEnumerable<ControllerActionEntity> controllerActionApplication)
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ControllerActionEntity, ControllerAction>();
                    cfg.IgnoreUnmapped();
                });

                IEnumerable<ControllerAction> listControllerActionApplication = Mapper.Map<IEnumerable<ControllerActionEntity>, IEnumerable<ControllerAction>>(controllerActionApplication);
                List<ControllerAction> listControllerActionDataBase = repository.getAll().Cast<ControllerAction>().ToList();

                List<ControllerAction> newControllerActions = listControllerActionApplication.
                    Where(ca => !listControllerActionDataBase.Any(db => db.action.Equals(ca.action) &&
                db.controller.Equals(ca.controller))).Cast<ControllerAction>().ToList();
                List<ControllerAction> deletedControllerActions = listControllerActionDataBase.
                    Where(ca => !listControllerActionApplication.Any(db => db.action.Equals(ca.action) &&
                db.controller.Equals(ca.controller))).Cast<ControllerAction>().ToList();



                repository.add(newControllerActions.GroupBy(ca => new { ca.controller, ca.action }).Select(group => group.First()).ToList());
                repository.delete(deletedControllerActions.Select(d => d.ControllerActionID).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ControllerActionEntity> GetActionsFromUser(UserEntity user)
        {
            try
            {
                var roles = roleService.getAll().ToList();
                var rcas = roleControllerActionService.getAll().ToList();
                var cas = getAll().ToList();
                var userroles = userRoleService.getAll().ToList();

                return from ur in userroles
                       join role in roles on ur.RoleID equals role.RoleID
                       join rca in rcas on role.RoleID equals rca.RoleID
                       join ca in cas on rca.ControllerActionID equals ca.ControllerActionID
                       where ur.UserID == user.UserID
                       select ca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

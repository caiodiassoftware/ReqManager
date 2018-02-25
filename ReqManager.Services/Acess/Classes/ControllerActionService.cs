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

namespace ReqManager.Services.Acess
{
    public class ControllerActionService : ServiceBase<ControllerAction, ControllerActionEntity>, IControllerActionService
    {
        private IUserRoleRepository urRepository { get; set; }
        private IRoleControllerActionRepository actionsRepository { get; set; }

        public ControllerActionService(
            IControllerActionRepository repository,
            IUserRoleRepository urRepository,
            IRoleControllerActionRepository actionsRepository,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.urRepository = urRepository;
            this.actionsRepository = actionsRepository;
        }


        public void Refresh(IEnumerable<ControllerActionEntity> controllerActionApplication)
        {
            try
            {
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

        public bool CanAccess(List<ControllerActionEntity> controllerActions, string controllerName, string actionName)
        {
            try
            {
                return controllerActions.Any(ca => ca.controller.Equals(controllerName + "Controller") && ca.action.Equals(actionName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControllerActionEntity> GetPermissions(int UserID)
        {
            try
            {
                List<ControllerAction> controllerActions = new List<ControllerAction>();

                var userRoles = urRepository.filter(u => u.UserID == UserID);
                var roles = userRoles.Select(r => r.Role);

                foreach (var role in roles)
                    controllerActions.AddRange(actionsRepository.filter(r => r.RoleID == role.RoleID).Select(a => a.ControllerAction));
                return convertEnumerableModelToEntity(controllerActions).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Services.InterfacesServices;
using ReqManager.Services.Estructure;
using System;
using System.Collections;
using System.Collections.Generic;
using ReqManager.Model;
using System.Reflection;
using System.Linq;

namespace ReqManager.Services.Acess
{
    public class ControllerActionService : ServiceBase<ControllerAction>, IControllerActionService
    {
        public ControllerActionService(IControllerActionRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }

        public void Refresh(IEnumerable controllerActionApplication)
        {
            try
            {
                List<ControllerAction> listControllerActionApplication = controllerActionApplication.Cast<ControllerAction>().ToList();
                List<ControllerAction> listControllerActionDataBase = ((IControllerActionRepository)repository).getAll().Cast<ControllerAction>().ToList();

                List<ControllerAction> newControllerActions = listControllerActionApplication.
                    Where(ca => !listControllerActionDataBase.Any(db => db.action.Equals(ca.action) &&
                db.controller.Equals(ca.controller))).Cast<ControllerAction>().ToList();
                List<ControllerAction> deletedControllerActions = listControllerActionDataBase.
                    Where(ca => !listControllerActionApplication.Any(db => db.action.Equals(ca.action) &&
                db.controller.Equals(ca.controller))).Cast<ControllerAction>().ToList();

                repository.add(newControllerActions);
                repository.delete(deletedControllerActions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

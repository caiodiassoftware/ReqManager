using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Services.InterfacesServices;
using ReqManager.Services.Estructure;
using System;
using System.Collections;
using System.Collections.Generic;
using ReqManager.Model;

namespace ReqManager.Services.Acess
{
    public class ControllerActionService : ServiceBase<CONTROLLER_ACTION>, IControllerActionService
    {
        public ControllerActionService(IControllerActionRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }

        public void refresh(IEnumerable controllerActionApplication)
        {
            try
            {
                //List<CONTROLLER_ACTION> listControllerActionApplication = controllerActionApplication.Cast<CONTROLLER_ACTION>().ToList();
                //List<CONTROLLER_ACTION> listControllerActionDataBase = GetAll().Cast<CONTROLLER_ACTION>().ToList();

                //List<CONTROLLER_ACTION> newControllerActions = listControllerActionApplication.Where(ca => !listControllerActionDataBase.Any(db => db.action.Equals(ca.action) && db.controller.Equals(ca.controller))).Cast<CONTROLLER_ACTION>().ToList();
                //List<CONTROLLER_ACTION> deletedControllerActions = listControllerActionDataBase.Where(ca => !listControllerActionApplication.Any(db => db.action.Equals(ca.action) && db.controller.Equals(ca.controller))).Cast<CONTROLLER_ACTION>().ToList();

                //foreach (CONTROLLER_ACTION ca in newControllerActions)
                //    repository.add(ca);
                //foreach (CONTROLLER_ACTION ca in deletedControllerActions)
                //    repository.delete(ca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

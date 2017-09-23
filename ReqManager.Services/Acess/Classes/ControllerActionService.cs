using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Services.InterfacesServices;
using ReqManager.Services.Estructure;
using System;
using System.Collections;
using System.Collections.Generic;
using ReqManager.Model;
using System.Linq;
using ReqManager.Entities;
using AutoMapper;
using ReqManager.Services.Extensions;

namespace ReqManager.Services.Acess
{
    public class ControllerActionService : ServiceBase<ControllerAction, ControllerActionEntity>, IControllerActionService
    {
        public ControllerActionService(IControllerActionRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

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

                repository.add(newControllerActions);
                repository.delete(deletedControllerActions.Select(d => d.ControllerActionID).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

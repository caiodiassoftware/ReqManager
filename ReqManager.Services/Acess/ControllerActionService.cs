using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ReqManager.Models;
using System.Collections;

namespace ReqManager.Services.Acess
{
    public class ControllerActionService : IControllerActionService
    {
        private readonly IControllerActionRepository repository;
        private readonly IUnitOfWork unit;

        public ControllerActionService(IControllerActionRepository repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
        }

        public IEnumerable<ControllerAction> filterByAction(string actionName)
        {
            try
            {
                return GetAll().Where(ca => ca.action.Equals(actionName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ControllerAction> filterByController(string controllerName)
        {
            try
            {
                return GetAll().Where(ca => ca.controller.Equals(controllerName));
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public ControllerAction Get(int id)
        {
            return GetAll().Where(ca => ca.ID_controllerAction == id).SingleOrDefault();
        }

        public IEnumerable<ControllerAction> GetAll()
        {
            try
            {
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void saveChanges()
        {
            unit.Commit();
        }

        public void refresh(IEnumerable controllerActionApplication)
        {
            try
            {
                List<ControllerAction> listControllerActionApplication = controllerActionApplication.Cast<ControllerAction>().ToList();
                List<ControllerAction> listControllerActionDataBase = GetAll().Cast<ControllerAction>().ToList();

                List<ControllerAction> newControllerActions = listControllerActionApplication.Where(ca => !listControllerActionDataBase.Any(db => db.action.Equals(ca.action) && db.controller.Equals(ca.controller))).Cast<ControllerAction>().ToList();
                List<ControllerAction> deletedControllerActions = listControllerActionDataBase.Where(ca => !listControllerActionApplication.Any(db => db.action.Equals(ca.action) && db.controller.Equals(ca.controller))).Cast<ControllerAction>().ToList();

                foreach (ControllerAction ca in newControllerActions)
                    repository.Add(ca);
                foreach (ControllerAction ca in deletedControllerActions)
                    repository.Delete(ca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void add(ControllerAction userRole)
        {
            throw new NotImplementedException();
        }

        public void edit(ControllerAction userRole)
        {
            throw new NotImplementedException();
        }

        public void delete(ControllerAction userRole)
        {
            throw new NotImplementedException();
        }

        public ControllerAction Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}

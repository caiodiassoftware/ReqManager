using ReqManager.Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReqManager.Models;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;

namespace ReqManager.Services.Acess
{
    public class RoleControllerActionService : IRoleControllerActionService
    {
        private readonly IRoleControllerActionRepository repository;
        private readonly IUnitOfWork unit;

        public RoleControllerActionService(IRoleControllerActionRepository repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
        }

        public void add(RoleControllerAction userRole)
        {
            repository.Add(userRole);
        }

        public void delete(RoleControllerAction userRole)
        {
            repository.Delete(userRole);
        }

        public void edit(RoleControllerAction userRole)
        {
            repository.Update(userRole);
        }

        public IEnumerable<RoleControllerAction> filterByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public RoleControllerAction Get(int? id)
        {
            return repository.GetById(Convert.ToInt32(id));
        }

        public IEnumerable<RoleControllerAction> GetAll()
        {
            return repository.GetAll();
        }

        public void saveChanges()
        {
            unit.Commit();
        }
    }
}

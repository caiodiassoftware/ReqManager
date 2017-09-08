using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReqManager.Models;

namespace ReqManager.Services.Acess
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;
        private readonly IUnitOfWork unit;

        public RoleService(IRoleRepository repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
        }

        public void add(Role userRole)
        {
            throw new NotImplementedException();
        }

        public void delete(Role userRole)
        {
            throw new NotImplementedException();
        }

        public void edit(Role userRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> filterByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Role Get(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            return repository.GetAll();
        }

        public void saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

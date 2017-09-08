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
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository repository;
        private readonly IUnitOfWork unit;

        public UserRoleService(IUserRoleRepository repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
        }

        public void add(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public void delete(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public void edit(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> filterByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> filterByUser(string roleName)
        {
            throw new NotImplementedException();
        }

        public UserRole Get(int? id)
        {
            throw new NotImplementedException();
        }

        public UserRole Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetAll()
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
    }
}

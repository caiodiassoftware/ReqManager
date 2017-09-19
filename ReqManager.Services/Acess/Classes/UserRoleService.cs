using System.Collections.Generic;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Task;

namespace ReqManager.Services.Acess.Classes
{
    public class UserRoleService : ServiceBase<UserRole, UserRoleEntity>, IUserRoleService
    {
        public UserRoleService(IUserRoleRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }

        public IEnumerable<Role> GetRoles()
        {
            return ((IUserRoleRepository)repository).GetRoles();
        }

        public IEnumerable<Users> GetUsers()
        {
            return ((IUserRoleRepository)repository).GetUsers();
        }
    }
}

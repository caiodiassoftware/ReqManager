using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.InterfacesRepositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        IEnumerable<Users> GetUsers();
        IEnumerable<Role> GetRoles();
    }
}

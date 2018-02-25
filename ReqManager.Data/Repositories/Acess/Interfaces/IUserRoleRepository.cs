using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Collections.Generic;

namespace ReqManager.Data.InterfacesRepositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        IEnumerable<UserRole> GetUserRoles(int UserID);
    }
}

using ReqManager.Model;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Acess.Interfaces
{
    public interface IUserRoleService : IService<UserRole>
    {
        IEnumerable<Users> GetUsers();
        IEnumerable<Role> GetRoles();
    }
}

using ReqManager.Model;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Acess.Interfaces
{
    public interface IUserRoleService : IService<UserRole>
    {
        IEnumerable<Users> GetUsers();
        IEnumerable<Role> GetRoles();
    }
}

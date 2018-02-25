using ReqManager.Entities.Acess;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Acess.Interfaces
{
    public interface IUserRoleService : IService<UserRoleEntity>
    {
        IEnumerable<UserRoleEntity> GetUserRoles(int UserID);
    }
}

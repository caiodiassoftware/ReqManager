using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Services.Acess.Classes
{
    public class UserRoleService : ServiceBase<UserRole, UserRoleEntity>, IUserRoleService
    {
        public UserRoleService(IUserRoleRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }

        public IEnumerable<UserRoleEntity> GetUserRoles(int UserID)
        {
            try
            {
                return filter(r => r.UserID == UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsInRole(int UserID, int RoleID)
        {
            try
            {
                return filter(r => r.UserID == UserID && r.RoleID == RoleID) != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

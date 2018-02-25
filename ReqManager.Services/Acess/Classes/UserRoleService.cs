using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;

namespace ReqManager.Services.Acess.Classes
{
    public class UserRoleService : ServiceBase<UserRole, UserRoleEntity>, IUserRoleService
    {
        private IUserRoleRepository repository { get; set; }

        public UserRoleService(IUserRoleRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
            this.repository = repository;
        }

        public IEnumerable<UserRoleEntity> GetUserRoles(int UserID)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.GetUserRoles(UserID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

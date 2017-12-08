using ReqManager.Services.Estructure;
using ReqManager.Data.Infrastructure;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using System.Linq;
using ReqManager.Entities.Acess;
using System;

namespace ReqManager.Services.Acess.Classes
{
    public class UserService : ServiceBase<Users, UserEntity>, IUserService
    {
        public UserService(IUserRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }

        public UserEntity Get(string login)
        {
            try
            {
                Users user = repository.filter(u => u.login.Equals(login)).FirstOrDefault();
                return convertModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserEntity Login(string login, string password)
        {
            try
            {
                Users user = repository.filter(u => u.login.Equals(login) && u.password.Equals(password) && u.active).FirstOrDefault();
                return convertModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

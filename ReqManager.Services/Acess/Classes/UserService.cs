using ReqManager.Services.Estructure;
using ReqManager.Data.Infrastructure;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using System.Linq;
using System.Collections.Generic;

namespace ReqManager.Services.Acess.Classes
{
    public class UserService : ServiceBase<Users>, IUserService
    {
        public UserService(IUserRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }

        public Users Login(string login, string password)
        {
            return null;
            //return repository.filter(u => u.login.Equals(login) && u.password.Equals(password)).FirstOrDefault();
        }
    }
}

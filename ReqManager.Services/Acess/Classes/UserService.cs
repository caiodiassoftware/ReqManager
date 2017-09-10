using ReqManager.Services.Estructure;
using ReqManager.Data.Infrastructure;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;

namespace ReqManager.Services.Acess.Classes
{
    public class UserService : ServiceBase<USERS>, IUserService
    {
        public UserService(IUserRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}

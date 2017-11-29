using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Acess;
using ReqManager.Data.Repositories.Requirements.Interfaces;

namespace ReqManager.Services.Acess.Classes
{
    public class RoleService : ServiceBase<Role, RoleEntity>, IRoleService
    {
        public RoleService(IRoleRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }
    }
}

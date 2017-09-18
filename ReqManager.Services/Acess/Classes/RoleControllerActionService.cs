using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Acess;

namespace ReqManager.Services.Acess.Classes
{
    public class RoleControllerActionService : ServiceBase<RoleControllerActionEntity>, IRoleControllerActionService
    {
        public RoleControllerActionService(IRoleControllerActionRepository repository, IUnitOfWork unit) : base(repository, unit)
        {

        }
    }
}

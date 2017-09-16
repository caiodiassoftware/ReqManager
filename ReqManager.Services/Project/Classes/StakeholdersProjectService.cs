using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Services.Project.Classes
{

    public class StakeholdersProjectService : ServiceBase<StakeholdersProject>, IStakeholdersProjectService
    {
        public StakeholdersProjectService(IStakeholdersProjectRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}

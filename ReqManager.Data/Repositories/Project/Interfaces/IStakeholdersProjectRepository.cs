using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Collections.Generic;

namespace ReqManager.Data.Repositories.Project.Interfaces
{
    public interface IStakeholdersProjectRepository : IRepository<StakeholdersProject>
    {
        IEnumerable<StakeholdersProject> getStakeholderByProject(int ProjectID);
    }
}

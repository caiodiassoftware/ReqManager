using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholdersProjectService : IService<StakeholdersProjectEntity>
    {
        IEnumerable<StakeholdersProjectEntity> getStakeholderByProject(int ProjectID);
        StakeholdersProjectEntity getByProjectAndUser(int ProjectID, int UserID);
    }
}

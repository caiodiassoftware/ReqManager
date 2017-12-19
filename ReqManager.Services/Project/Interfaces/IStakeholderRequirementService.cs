using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholderRequirementService : IService<StakeholderRequirementEntity>
    {
        IEnumerable<StakeholderRequirementEntity> getStakeholdersFromRequirement(int RequirementID);
        StakeholderRequirementEntity get(int UserID, int RequirementID);
    }
}

using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholderRequirementService : IService<StakeholderRequirementEntity>
    {
        StakeholderRequirementEntity filterByUser(int UserID);
        StakeholderRequirementEntity filterByRequirementAndUser(int RequirementID, int UserID);
    }
}

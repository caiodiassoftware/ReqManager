using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholderRequirementService : IService<StakeholderRequirementApprovalEntity>
    {
        StakeholderRequirementApprovalEntity filterByUser(int UserID);
    }
}

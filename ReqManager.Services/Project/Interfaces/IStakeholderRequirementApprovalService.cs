using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholderRequirementApprovalService : IService<StakeholderRequirementApprovalEntity>
    {
        StakeholderRequirementApprovalEntity filterByUser(int UserID);
    }
}

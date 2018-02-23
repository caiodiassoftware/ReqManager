using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholderRequirementApprovalService : IService<StakeholderRequirementApprovalEntity>
    {
        StakeholderRequirementApprovalEntity filterByUser(int UserID);
        List<StakeholderRequirementApprovalEntity> filterByRequirement(int RequirementID);
    }
}

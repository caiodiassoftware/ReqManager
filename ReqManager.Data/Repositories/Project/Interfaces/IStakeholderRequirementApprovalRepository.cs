using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Collections.Generic;

namespace ReqManager.Data.Repositories.Project.Interfaces
{
    public interface IStakeholderRequirementApprovalRepository : IRepository<StakeholderRequirementApproval>
    {
        List<StakeholderRequirementApproval> filterByRequirement(int RequirementID);
    }
}

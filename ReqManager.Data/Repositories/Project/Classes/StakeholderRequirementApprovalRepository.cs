using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Project.Classes
{
    public class StakeholderRequirementApprovalRepository : RepositoryBase<StakeholderRequirementApproval>, IStakeholderRequirementApprovalRepository
    {
        public StakeholderRequirementApprovalRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

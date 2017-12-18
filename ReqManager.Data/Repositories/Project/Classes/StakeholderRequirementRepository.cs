using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Project.Classes
{
    public class StakeholderRequirementRepository : RepositoryBase<StakeholderRequirement>, IStakeholderRequirementRepository
    {
        public StakeholderRequirementRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

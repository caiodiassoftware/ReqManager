using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Project.Classes
{
    public class StakeholderClassificationRepository : RepositoryBase<StakeholderClassification>, IStakeholderClassificationRepository
    {
        public StakeholderClassificationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

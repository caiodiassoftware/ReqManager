using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementSubTypeRepository : RepositoryBase<RequirementSubType>, IRequirementSubTypeRepository
    {
        public RequirementSubTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

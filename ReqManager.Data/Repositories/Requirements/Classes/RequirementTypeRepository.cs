using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementTypeRepository : RepositoryBase<RequirementType>, IRequirementTypeRepository
    {
        public RequirementTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

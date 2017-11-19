using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementRequestForChangesRepository : 
        RepositoryBase<RequirementRequestForChanges>, IRequirementRequestForChangesRepository
    {
        public RequirementRequestForChangesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

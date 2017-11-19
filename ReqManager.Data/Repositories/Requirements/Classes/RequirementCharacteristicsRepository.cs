using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementCharacteristicsRepository : 
        RepositoryBase<RequirementCharacteristics>, 
        IRequirementCharacteristicsRepository
    {
        public RequirementCharacteristicsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

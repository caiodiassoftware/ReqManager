using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementRequestForChangesRepository : 
        RepositoryBase<RequirementRequestForChanges>, IRequirementRequestForChangesRepository
    {
        public RequirementRequestForChangesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<RequirementRequestForChanges> filterByRequirement(int RequirementID)
        {
            try
            {
                return DbContext.RequirementRequestForChanges.Where(r => r.StakeholderRequirement.RequirementID == RequirementID).ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

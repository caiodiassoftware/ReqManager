using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Data.Repositories.Project.Classes
{
    public class StakeholderRequirementApprovalRepository : 
        RepositoryBase<StakeholderRequirementApproval>, 
        IStakeholderRequirementApprovalRepository
    {
        public StakeholderRequirementApprovalRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<StakeholderRequirementApproval> filterByRequirement(int RequirementID)
        {
            try
            {
                return DbContext.StakeholderRequirementApproval.Where(r => r.StakeholderRequirement.RequirementID == RequirementID).ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Collections.Generic;

namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementRequestForChangesRepository : IRepository<RequirementRequestForChanges>
    {
        List<RequirementRequestForChanges> filterByRequirement(int RequirementID);
    }
}

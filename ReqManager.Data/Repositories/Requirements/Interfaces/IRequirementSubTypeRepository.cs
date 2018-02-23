using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Collections.Generic;

namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementSubTypeRepository : IRepository<RequirementSubType>
    {
        List<RequirementSubType> filterByRequirementType(int RequirementTypeID);
    }
}

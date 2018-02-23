using ReqManager.Entities.Requirement;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementRequestForChangesService : IService<RequirementRequestForChangesEntity>
    {
        bool validateRequestForRequirement(int RequirementID);
        List<RequirementRequestForChangesEntity> filterByRequirement(int RequirementID);
    }
}

using ReqManager.Entities.Requirement;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementSubTypeService : IService<RequirementSubTypeEntity>
    {
        List<RequirementSubTypeEntity> filterByRequirementType(int RequirementTypeID);
    }
}

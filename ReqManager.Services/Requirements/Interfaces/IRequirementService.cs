using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementService : IService<RequirementEntity>
    {
        void update(ref RequirementEntity entity, int RequirementRequestForChangesID, string rationale);
        void add(ref RequirementEntity requirement, ref ProjectRequirementsEntity projectRequirement);
    }
}

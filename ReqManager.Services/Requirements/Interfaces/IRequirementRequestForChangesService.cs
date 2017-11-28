using ReqManager.Entities.Requirement;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementRequestForChangesService : IService<RequirementRequestForChangesEntity>
    {
        bool validateRequestForRequirement(int RequirementID);
    }
}

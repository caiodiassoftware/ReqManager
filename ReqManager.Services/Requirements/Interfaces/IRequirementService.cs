using ReqManager.Entities.Requirement;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementService : IService<RequirementEntity>
    {
        void update(ref RequirementEntity entity, string userLogin);
    }
}

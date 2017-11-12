using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IProjectRequirementsService : IService<ProjectRequirementsEntity>
    {
        bool isTraceable(int ProjectID, int RequirementID);
    }
}

using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IProjectRequirementsService : IService<ProjectRequirementsEntity>
    {
        bool isTraceable(int ProjectID, int RequirementID);
        IEnumerable<ProjectRequirementsEntity> getRequirementsByProject(int ProjectID);
        ProjectRequirementsEntity getRequirementsByProjectAndRequirement(int ProjectID, int RequirementID);
        IEnumerable<ProjectRequirementsEntity> getRequirementsByRequirement(int RequirementID);
    }
}
